using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <returns>実行結果</returns>
        public bool LoadSetting()
        {
            settingData = new SettingModel().Load();
            return settingData != null;
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool SaveSetting()
        {
            return new SettingModel().Save(settingData);
        }

        public void ExecuteClassification()
        {
            new ClassificationModel().Execute(settingData);
        }

        /*
        public void UpdateFromFolder(string path)
        {
            settingData.FromFolder = path;
        }

        public void UpdateToFolder(string path)
        {
            settingData.ToFolder = path;
        }

        public void UpdateCreateFolderIfNotExist(bool enable)
        {
            settingData.CreateFolderIfNotExist = enable;
        }

        public bool UpdateClassifyAs(string id, string folderName)
        {
            bool result = true;

            return result;
        }

        public bool DeleteClassifyAs(string id, string folderName)
        {
            bool result = true;

            return result;
        }
        */

        #endregion
    }
}
