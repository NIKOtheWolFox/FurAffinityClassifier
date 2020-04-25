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

        public Dictionary<string, string> ClassifyAs
        {
            get
            {
                var dd = new Dictionary<string, string>();
                foreach(var tt in settingData.IdFolderMappings)
                {
                    dd.Add(tt.Id, tt.FolderName);
                }
                return dd;
            }

            set
            {
                settingData.IdFolderMappings.Clear();
                foreach(var tt in value)
                {
                    settingData.IdFolderMappings.Add(new IdFolderMappingData()
                    {
                        Id = tt.Key,
                        FolderName = tt.Value,
                    });
                }
            }
        }

        #endregion

        #region Public Method

        public bool LoadSetting()
        {
            settingData = new SettingModel().Load();
            return settingData != null;
        }

        public bool SaveSetting()
        {
            return new SettingModel().Save(settingData);
        }

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

        #endregion
    }
}
