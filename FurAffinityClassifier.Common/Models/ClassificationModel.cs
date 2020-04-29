using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FurAffinityClassifier.Common.Datas;
using log4net;

namespace FurAffinityClassifier.Common.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel
    {
        #region Private Field

        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClassificationModel));

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

        /// <summary>
        /// 分類を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool Execute()
        {
            var result = true;

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

                    File.Move(
                        file,
                        Path.Combine(settingData.ToFolder, folderName, Path.GetFileName(file)));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                result = false;
            }

            return result;
        }

        #endregion
    }
}
