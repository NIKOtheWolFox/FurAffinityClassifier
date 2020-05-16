using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FurAffinityClassifier.Common.Datas;
using NLog;

namespace FurAffinityClassifier.Common.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel
    {
        #region Private Field

        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 設定データ
        /// </summary>
        private SettingData settingData = new SettingData();

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingData">設定データ</param>
        public ClassificationModel(SettingData settingData)
        {
            this.settingData = settingData;
        }

        #endregion

        #region Public Method

        public async Task<Dictionary<string, int>> ExecuteAsync()
        {
            var classificationResults = new List<ClassificationResult>();

            try
            {
                var files = Directory.GetFiles(settingData.FromFolder);

                using (var semaphore = new SemaphoreSlim(5))
                {
                    var tasks = files.Select(async file =>
                    {
                        await semaphore.WaitAsync();
                        var classificationResult = new ClassificationResult(file);
                        try
                        {
                            var match = Regex.Match(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]{3,}?)_.*");
                            if (!match.Success)
                            {
                                return classificationResult;
                            }

                            classificationResult.Targeted = true;
                            var id = match.Groups["id"].Value;

                            var folderName = string.Empty;
                            if (settingData.ClassifyAsDatas.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                            {
                                folderName = settingData.ClassifyAsDatas
                                    .Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault()
                                    .Folder;
                            }
                            else
                            {
                                var matchedFolder = Directory.GetDirectories(settingData.ToFolder)
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

                            if (string.IsNullOrEmpty(folderName))
                            {
                                if (settingData.CreateFolderIfNotExist)
                                {
                                    folderName = id.TrimEnd('.');
                                    Directory.CreateDirectory(Path.Combine(settingData.ToFolder, folderName));
                                }
                                else
                                {
                                    return classificationResult;
                                }
                            }

                            var classifiedFileName = Path.Combine(settingData.ToFolder, folderName, Path.GetFileName(file));
                            if (File.Exists(classifiedFileName))
                            {
                                if (settingData.OverwriteIfExist)
                                {
                                    File.Delete(classifiedFileName);
                                }
                                else
                                {
                                    return classificationResult;
                                }
                            }

                            File.Move(file, classifiedFileName);

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
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }

            return new Dictionary<string, int>()
            {
                { Const.ClassificationResultFoundFileCount, classificationResults.Count() },
                { Const.ClassificationResultTargetFileCount, classificationResults.Count(x => x.Targeted) },
                { Const.ClassificationResultClassifiedFileCount, classificationResults.Count(x => x.Classified) },
            };
        }

        /// <summary>
        /// 分類を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public Dictionary<string, int> Execute()
        {
            var result = new Dictionary<string, int>()
            {
                { Const.ClassificationResultFoundFileCount, 0 },
                { Const.ClassificationResultTargetFileCount, 0 },
                { Const.ClassificationResultClassifiedFileCount, 0 },
            };

            try
            {
                var foundFiles = Directory.GetFiles(settingData.FromFolder);
                result[Const.ClassificationResultFoundFileCount] = foundFiles.Count();

                var targetFiles = foundFiles.Where(f => Regex.IsMatch(Path.GetFileName(f), @"[0-9]+\.[a-z0-9-~^.]{3,}_.*"));
                result[Const.ClassificationResultTargetFileCount] = targetFiles.Count();

                foreach (var file in targetFiles)
                {
                    try
                    {
                        var match = Regex.Match(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]{3,}?)_.*");
                        if (!match.Success)
                        {
                            continue;
                        }

                        var id = match.Groups["id"].Value;

                        var folderName = string.Empty;
                        if (settingData.ClassifyAsDatas.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                        {
                            folderName = settingData.ClassifyAsDatas
                                .Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault()
                                .Folder;
                        }
                        else
                        {
                            var matchedFolder = Directory.GetDirectories(settingData.ToFolder)
                                .Where(f => id.TrimEnd('.') == Path.GetFileName(f).ToLower().Replace("_", string.Empty));
                            if (matchedFolder.Count() > 1)
                            {
                                Logger.Warn($"Multiple folders weere found for file {file} (ID={id}), skipped");
                                continue;
                            }
                            else if (matchedFolder.Count() == 1)
                            {
                                folderName = Path.GetFileName(matchedFolder.First());
                            }
                        }

                        if (string.IsNullOrEmpty(folderName))
                        {
                            if (settingData.CreateFolderIfNotExist)
                            {
                                folderName = id.TrimEnd('.');
                                Directory.CreateDirectory(Path.Combine(settingData.ToFolder, folderName));
                            }
                            else
                            {
                                continue;
                            }
                        }

                        var classifiedFileName = Path.Combine(settingData.ToFolder, folderName, Path.GetFileName(file));
                        if (File.Exists(classifiedFileName))
                        {
                            if (settingData.OverwriteIfExist)
                            {
                                File.Delete(classifiedFileName);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        File.Move(file, classifiedFileName);

                        result[Const.ClassificationResultClassifiedFileCount]++;
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Error occurred while classifying {file}");
                        Logger.Error(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }

            return result;
        }

        #endregion

        private class ClassificationResult
        {
            public ClassificationResult()
            {
                Filename = string.Empty;
                Targeted = false;
                Classified = false;
            }

            public ClassificationResult(string filename)
                : this()
            {
                Filename = filename;
            }

            public string Filename { get; set; }
            public bool Targeted { get; set; }
            public bool Classified { get; set; }
        }
    }
}
