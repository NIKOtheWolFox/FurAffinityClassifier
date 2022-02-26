using System.Collections.Generic;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// アプリケーションの機能
    /// </summary>
    public class AppModel : IAppModel
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Modelのインスタンス</param>
        /// <param name="classificationModel">分類Modelのインスタンス</param>
        public AppModel(ISettingsModel settingsModel, IClassificationModel classificationModel)
        {
            SettingsModel = settingsModel;
            ClassificationModel = classificationModel;
        }

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder
        {
            get
            {
                return SettingsModel.SettingsData.FromFolder;
            }

            set
            {
                if (value != SettingsModel.SettingsData.FromFolder)
                {
                    SettingsModel.SettingsData.FromFolder = value;
                }
            }
        }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder
        {
            get
            {
                return SettingsModel.SettingsData.ToFolder;
            }

            set
            {
                if (value != SettingsModel.SettingsData.ToFolder)
                {
                    SettingsModel.SettingsData.ToFolder = value;
                }
            }
        }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist
        {
            get
            {
                return SettingsModel.SettingsData.CreateFolderIfNotExist;
            }

            set
            {
                if (value != SettingsModel.SettingsData.CreateFolderIfNotExist)
                {
                    SettingsModel.SettingsData.CreateFolderIfNotExist = value;
                }
            }
        }

        /// <summary>
        /// IDをFur Affinityから取得するか
        /// </summary>
        public bool GetIdFromFurAffinity
        {
            get
            {
                return SettingsModel.SettingsData.GetIdFromFurAffinity;
            }

            set
            {
                if (value != SettingsModel.SettingsData.GetIdFromFurAffinity)
                {
                    SettingsModel.SettingsData.GetIdFromFurAffinity = value;
                }
            }
        }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public bool OverwriteIfExist
        {
            get
            {
                return SettingsModel.SettingsData.OverwriteIfExist;
            }

            set
            {
                if (value != SettingsModel.SettingsData.OverwriteIfExist)
                {
                    SettingsModel.SettingsData.OverwriteIfExist = value;
                }
            }
        }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas
        {
            get
            {
                return SettingsModel.SettingsData.ClassifyAsDatas;
            }

            set
            {
                if (value != SettingsModel.SettingsData.ClassifyAsDatas)
                {
                    SettingsModel.SettingsData.ClassifyAsDatas = value;
                }
            }
        }

        /// <summary>
        /// 設定Model
        /// </summary>
        private ISettingsModel SettingsModel { get; }

        /// <summary>
        /// 分類Model
        /// </summary>
        private IClassificationModel ClassificationModel { get; }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool LoadSettings()
        {
            return SettingsModel.LoadFromFile();
        }

        /// <summary>
        /// 非同期で設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> LoadSettingsAsync()
        {
            return await SettingsModel.LoadFromFileAsync();
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool SaveSettings()
        {
            return SettingsModel.SaveToFile();
        }

        /// <summary>
        /// 非同期で設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> SaveSettingsAsync()
        {
            return await SettingsModel.SaveToFileAsync();
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        public bool ValidateSettings()
        {
            return SettingsModel.Validate();
        }

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を格納したValueTuple</returns>
        public async Task<(int foundFiles, int targetFiles, int classifiedFiles)> ClassifyAsync()
        {
            return await ClassificationModel.ExecuteAsync(SettingsModel.SettingsData);
        }
    }
}
