using System.Collections.Generic;
using System.Threading.Tasks;
using FurAffinityClassifier.Common.Datas;

namespace FurAffinityClassifier.Common.Models
{
    /// <summary>
    /// アプリケーションの機能
    /// </summary>
    public class AppModel
    {
        #region Public Property

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder
        {
            get => SettingModel.FromFolder;
            set => SettingModel.FromFolder = value;
        }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder
        {
            get => SettingModel.ToFolder;
            set => SettingModel.ToFolder = value;
        }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist
        {
            get => SettingModel.CreateFolderIfNotExist;
            set => SettingModel.CreateFolderIfNotExist = value;
        }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public bool OverwriteIfExist
        {
            get => SettingModel.OverwriteIfExist;
            set => SettingModel.OverwriteIfExist = value;
        }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas
        {
            get => SettingModel.ClassifyAsDatas;
            set => SettingModel.ClassifyAsDatas = value;
        }

        #endregion

        #region Private Property

        /// <summary>
        /// 設定機能
        /// </summary>
        private SettingModel SettingModel { get; } = new SettingModel();

        #endregion

        #region Public Method

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        public void LoadSetting()
        {
            SettingModel.LoadFromFile();
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool SaveSetting()
        {
            return SettingModel.SaveToFile();
        }

        /// <summary>
        /// 設定を非同期で保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public async Task<bool> SaveSettingAsync()
        {
            return await SettingModel.SaveToFileAsync();
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>検証結果</returns>
        public bool ValidateSetting()
        {
            return SettingModel.Validate();
        }

        /// <summary>
        /// 分類する
        /// </summary>
        /// <returns>実行結果</returns>
        public Dictionary<string, int> Classify()
        {
            return new ClassificationModel(SettingModel.SettingData).Execute();
        }

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <returns>実行結果</returns>
        public async Task<Dictionary<string, int>> ClassifyAsync()
        {
            return await new ClassificationModel(SettingModel.SettingData).ExecuteAsync();
        }

        #endregion
    }
}
