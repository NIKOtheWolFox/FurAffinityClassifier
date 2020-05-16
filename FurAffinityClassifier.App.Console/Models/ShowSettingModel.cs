using System;
using System.Text;
using FurAffinityClassifier.App.Console.Properties;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Models;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// --show-settingオプション
    /// </summary>
    public class ShowSettingModel
    {
        #region Private Property

        private AppModel appModel;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="appModel">アプリケーションの機能</param>
        public ShowSettingModel(AppModel appModel)
        {
            this.appModel = appModel;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            var builder = new StringBuilder();

            var classifyAs = string.Empty;
            foreach(var ca in appModel.ClassifyAsDatas)
            {
                classifyAs += $"{ca.Id} -> {ca.Folder}{Environment.NewLine}";
            }

            builder.Append(
                string.Format(
                    Resources.MessageShowSetting,
                    appModel.FromFolder,
                    appModel.ToFolder,
                    appModel.CreateFolderIfNotExist,
                    appModel.OverwriteIfExist,
                    Environment.NewLine + classifyAs));

            return builder.ToString();
        }

        #endregion
    }
}
