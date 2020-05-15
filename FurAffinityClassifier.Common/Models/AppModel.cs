using System.Collections.Generic;

namespace FurAffinityClassifier.Common.Models
{
    /// <summary>
    /// アプリケーションの機能
    /// </summary>
    public class AppModel
    {
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

        #endregion
    }
}
