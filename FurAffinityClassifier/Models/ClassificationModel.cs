using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using FurAffinityClassifier.Datas;
using NLog;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel : IClassificationModel
    {
        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 分類を非同期で実行する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        public async Task<Dictionary<string, int>> ExecuteAsync(SettingsData settingsData)
        {
            List<ClassificationResult> classificationResults = new ();

            try
            {
                string[] files = Directory.GetFiles(settingsData.FromFolder);

                if (!await CreateFolderAsync(settingsData, files))
                {
                    throw new Exception("Error while create folder.");
                }

                using SemaphoreSlim semaphore = new (5);
                var tasks = files.Select(async file =>
                {
                    await semaphore.WaitAsync();
                    ClassificationResult classificationResult = new (file);
                    try
                    {
                        Match match = Regex.Match(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]+?)_.*");
                        if (!match.Success)
                        {
                            return classificationResult;
                        }

                        classificationResult.Targeted = true;
                        string id = match.Groups["id"].Value;

                        string folderName = string.Empty;
                        if (settingsData.ClassifyAsDatas.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                        {
                            folderName = settingsData.ClassifyAsDatas
                                .Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault()
                                .Folder;
                        }
                        else
                        {
                            var matchedFolder = Directory.GetDirectories(settingsData.ToFolder)
                                .Where(f => id.TrimEnd('.') == Path.GetFileName(f).ToLower().Replace("_", string.Empty));
                            if (matchedFolder.Count() > 1)
                            {
                                Logger.Warn($"Multiple folders weere found for file {file} (ID={id}), skipped");
                                return classificationResult;
                            }
                            else if (matchedFolder.Count() == 1)
                            {
                                folderName = Path.GetFileName(matchedFolder.First());
                            }
                        }

                        /*
                        if (string.IsNullOrEmpty(folderName))
                        {
                            if (settingsData.CreateFolderIfNotExist)
                            {
                                folderName = id.TrimEnd('.');
                                Directory.CreateDirectory(Path.Combine(settingsData.ToFolder, folderName));
                            }
                            else
                            {
                                return classificationResult;
                            }
                        }
                        */
                        if (string.IsNullOrEmpty(folderName))
                        {
                            return classificationResult;
                        }

                        string classifiedFileName = Path.Combine(settingsData.ToFolder, folderName, Path.GetFileName(file));
                        if (File.Exists(classifiedFileName))
                        {
                            if (settingsData.OverwriteIfExist)
                            {
                                File.Delete(classifiedFileName);
                            }
                            else
                            {
                                return classificationResult;
                            }
                        }

                        // File.Move(file, classifiedFileName);

                        classificationResult.Classified = true;
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e.ToString());
                    }
                    finally
                    {
                        semaphore.Release();
                    }

                    return classificationResult;
                });

                classificationResults = (await Task.WhenAll(tasks)).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }

            return new Dictionary<string, int>()
            {
                { Const.ClassificationResultFoundFileCount, classificationResults.Count },
                { Const.ClassificationResultTargetFileCount, classificationResults.Count(x => x.Targeted) },
                { Const.ClassificationResultClassifiedFileCount, classificationResults.Count(x => x.Classified) },
            };
        }

        /// <summary>
        /// フォルダーを作成する
        /// </summary>
        /// <returns>true:すべて成功/false:失敗あり</returns>
        private async Task<bool> CreateFolderAsync(SettingsData settingsData, string[] files)
        {
            if (!settingsData.CreateFolderIfNotExist)
            {
                Logger.Info("no create");
                return true;
            }

            bool result = true;
            try
            {
                var ids = files.Where(file => Regex.IsMatch(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]+?)_.*"))
                    .Select(file => Regex.Match(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]+?)_.*").Groups["id"].Value)
                    .Distinct();

                IConfiguration config = Configuration.Default
                    .WithDefaultLoader();
                using IBrowsingContext context = BrowsingContext.New(config);

                await Parallel.ForEachAsync(ids, new ParallelOptions { MaxDegreeOfParallelism = 5 }, async (id, ct) =>
                {
                    if (settingsData.ClassifyAsDatas.Any(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                    {
                        string folderName = settingsData.ClassifyAsDatas
                            .Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower())
                            .FirstOrDefault()
                            .Folder;
                        if (Directory.Exists(Path.Combine(settingsData.ToFolder, folderName)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        var matchedFolder = Directory.GetDirectories(settingsData.ToFolder)
                            .Where(f => id.TrimEnd('.') == Path.GetFileName(f).ToLower().Replace("_", string.Empty));
                        if (matchedFolder.Count() > 1)
                        {
                            return;
                        }
                        else if (matchedFolder.Count() == 1)
                        {
                            string folderName = Path.GetFileName(matchedFolder.First());
                            if (Directory.Exists(Path.Combine(settingsData.ToFolder, folderName)))
                            {
                                return;
                            }
                        }
                    }

                    if (settingsData.GetIdFromFurAffinity)
                    {
                        IDocument doc = await context.OpenAsync($"https://www.furaffinity.net/user/{id}/", ct);
                        string originalId = doc.Title.Replace("Userpage of", string.Empty).Replace("-- Fur Affinity [dot] net", string.Empty).Trim();
                        Directory.CreateDirectory(Path.Combine(settingsData.ToFolder, originalId));
                    }
                    else
                    {
                        string folderName = id.TrimEnd('.');
                        Directory.CreateDirectory(Path.Combine(settingsData.ToFolder, folderName));
                    }
                });
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 分類結果
        /// </summary>
        private class ClassificationResult
        {
            /// <summary>
            /// コンストラクター
            /// </summary>
            public ClassificationResult()
            {
                Filename = string.Empty;
                Targeted = false;
                Classified = false;
            }

            /// <summary>
            /// コンストラクター
            /// </summary>
            /// <param name="filename">ファイル名</param>
            public ClassificationResult(string filename)
                : this()
            {
                Filename = filename;
            }

            /// <summary>
            /// ファイル名
            /// </summary>
            public string Filename { get; set; }

            /// <summary>
            /// 対象か
            /// </summary>
            public bool Targeted { get; set; }

            /// <summary>
            /// 分類したか
            /// </summary>
            public bool Classified { get; set; }
        }
    }
}
