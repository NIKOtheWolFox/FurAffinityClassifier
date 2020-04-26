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
