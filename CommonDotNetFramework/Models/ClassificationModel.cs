using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FurAffinityClassifier.CommonDotNetFramework.Datas;

namespace FurAffinityClassifier.CommonDotNetFramework.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel
    {
        #region Public Method

        /// <summary>
        /// 分類を実行する
        /// </summary>
        /// <param name="settingData">設定データ</param>
        /// <returns>実行結果</returns>
        public bool Execute(SettingData settingData)
        {
            bool result = true;

            //// TODO : 不要なコメントの削除
            //// TODO : 不要なコンソール出力の削除(ログ出力？)
            //// TODO : リファクタリング

            try
            {
                var files = Directory.GetFiles(settingData.FromFolder)
                    .Where(f => Regex.IsMatch(Path.GetFileName(f), @"[0-9]+\.[a-z0-9-~^.]{3,}_.*"));
                foreach (var file in files)
                {
                    var match = Regex.Match(Path.GetFileName(file), @"[0-9]+\.(?<id>[a-z0-9-~^.]{3,}?)_.*");
                    if (!match.Success)
                    {
                        continue;
                    }

                    var id = match.Groups["id"].Value;

                    var folderName = string.Empty;
                    if (settingData.IdFolderMappings.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                    {
                        folderName = settingData.IdFolderMappings
                            .Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault()
                            .FolderName;
                    }
                    else
                    {
                        var matchedFolder = Directory.GetDirectories(settingData.ToFolder)
                            .Where(f => id.TrimEnd('.') == Path.GetFileName(f).ToLower().Replace("_", string.Empty));
                        if (matchedFolder.Count() > 1)
                        {
                            // Should I log here?
                            continue;
                        }
                        else if (matchedFolder.Count() == 1)
                        {
                            folderName = Path.GetFileName(matchedFolder.First());
                        }
                    }

                    Console.WriteLine($"folder for ID({id}) is to {(string.IsNullOrEmpty(folderName) ? "<not found>" : folderName)}");

                    if (string.IsNullOrEmpty(folderName))
                    {
                        if (settingData.CreateFolderIfNotExist)
                        {
                            Directory.CreateDirectory(Path.Combine(settingData.ToFolder, folderName));
                        }
                        else
                        {
                            continue;
                        }
                    }

                    File.Move(
                        file,
                        Path.Combine(settingData.ToFolder, folderName, Path.GetFileName(file)));
                }

                /*
                foreach (var f in files2)
                {
                    var match = Regex.Match(f, @"[0-9]+\.(?<id>[a-z0-9-~^.]{3,}?)_.*");
                    var id = string.Empty;
                    if (match.Success)
                    {
                        id = match.Groups["id"].Value;
                        ////Console.WriteLine($"id from {f} is {match.Groups["id"].Value}");
                    }
                    else
                    {
                        ////Console.WriteLine($"cannot find id from {f}");
                        continue;
                    }

                    string foldername = id;
                    if (settingData.IdFolderMappings.Exists(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()))
                    {
                        foldername = settingData.IdFolderMappings.Where(mapping => id == mapping.Id.Replace("_", string.Empty).ToLower()).FirstOrDefault().FolderName;
                        ////Console.WriteLine($"ID({id}) is to {foldername}");
                    }

                    ////string folderpath = Path.Combine(settingData.ToFolder, foldername);
                    ////Console.WriteLine($"path {folderpath} exist? : {Directory.Exists(folderpath)}");
                    var foldersOnTo = Directory.GetDirectories(settingData.ToFolder);
                    var folderTo = string.Empty;
                    var matchedfolder = foldersOnTo.Where(ff => foldername.TrimEnd('.') == Path.GetFileName(ff).ToLower().Replace("_", string.Empty));
                    if (matchedfolder.Count() > 1)
                    {
                        Console.WriteLine($"multiple fromders for {foldername}");
                        continue;
                    }
                    else if (matchedfolder.Count() == 1)
                    {
                        folderTo = matchedfolder.First();
                        Console.WriteLine($"folder for ID({id}) is to {folderTo}");
                    }

                    if (string.IsNullOrEmpty(folderTo))
                    {
                        if (settingData.CreateFolderIfNotExist)
                        {
                            Console.WriteLine($"I have to create filder {foldername}");
                            Directory.CreateDirectory(Path.Combine(settingData.ToFolder, foldername.TrimEnd('.')));
                            folderTo = Path.Combine(settingData.ToFolder, foldername.TrimEnd('.'));
                        }
                        else
                        {
                            Console.WriteLine("no folder, continue");
                            continue;
                        }
                    }

                    var filename = Path.GetFileName(f);
                    File.Move(f, Path.Combine(folderTo, filename));

                    Console.WriteLine($"DONE : {f} -> {Path.Combine(folderTo, filename)}");
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
