using System.Collections.Generic;
using System.IO;
using System.Linq;
using FurAffinityClassifier.AppWindowsForms.Datas;
using FurAffinityClassifier.CommonDotNetFramework.Datas;
using FurAffinityClassifier.CommonDotNetFramework.Models;

namespace FurAffinityClassifier.AppWindowsForms.ViewModels
{
    /// <summary>
    /// メイン画面用ViewModel
    /// </summary>
    internal class MainFormViewModel
    {
        #region Private Field

        /// <summary>
        /// 設定データ
        /// </summary>
        private SettingData settingData = new SettingData();

        #endregion

        #region Public Property

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder
        {
            get
            {
                return settingData.FromFolder;
            }

            set
            {
                settingData.FromFolder = value;
            }
        }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder
        {
            get
            {
                return settingData.ToFolder;
            }

            set
            {
                settingData.ToFolder = value;
            }
        }

        /// <summary>
        /// フォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist
        {
            get
            {
                return settingData.CreateFolderIfNotExist;
            }

            set
            {
                settingData.CreateFolderIfNotExist = value;
            }
        }

        /// <summary>
        /// IDと異なるフォルダーに振り分ける設定
        /// </summary>
        public Dictionary<string, string> ClassifyAs
        {
            get
            {
                return settingData.IdFolderMappings.ToDictionary(
                    mapping => mapping.Id,
                    mapping => mapping.FolderName);
            }

            set
            {
                settingData.IdFolderMappings = value
                    .Select(
                        kvp => new IdFolderMappingData()
                        {
                            Id = kvp.Key,
                            FolderName = kvp.Value
                        })
                    .ToList();
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        public void LoadSetting()
        {
            settingData = new SettingModel().Load();
            if (settingData == null)
            {
                settingData = new SettingData();
            }
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool SaveSetting()
        {
            return new SettingModel().Save(settingData);
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>検証結果</returns>
        public Dictionary<string, bool> ValidateSetting()
        {
            var result = new Dictionary<string, bool>
            {
                { Const.ValidationResultKeyFromFolder, true },
                { Const.ValidationResultKeyToFolder, true },
                { Const.ValidationResultKeyMapping, true },
            };

            if (string.IsNullOrEmpty(settingData.FromFolder)
                || !Directory.Exists(settingData.FromFolder))
            {
                result[Const.ValidationResultKeyFromFolder] = false;
            }

            if (string.IsNullOrEmpty(settingData.ToFolder)
                || !Directory.Exists(settingData.ToFolder))
            {
                result[Const.ValidationResultKeyToFolder] = false;
            }

            if (settingData.IdFolderMappings.Exists(mapping => string.IsNullOrEmpty(mapping.Id))
                || settingData.IdFolderMappings.Exists(mapping => string.IsNullOrEmpty(mapping.FolderName)))
            {
                result[Const.ValidationResultKeyMapping] = false;
            }

            return result;
        }

        /// <summary>
        /// 分類を実行する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool ExecuteClassification()
        {
            return new ClassificationModel().Execute(settingData);
        }

        #endregion
    }
}
