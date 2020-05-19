using System.Text;
using FurAffinityClassifier.App.Console.Properties;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Models;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// オプションなし
    /// </summary>
    public class DefaultModel
    {
        #region Private Property

        /// <summary>
        /// アプリケーションの機能
        /// </summary>
        private AppModel appModel;

        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="appModel">アプリケーションの機能</param>
        public DefaultModel(AppModel appModel)
        {
            this.appModel = appModel;
        }

        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            if (!appModel.ValidateSetting())
            {
                return Resources.MessageInvalidSetting;
            }

            var classificationResult = appModel.Classify();
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(Resources.MessageClassifyFileDone);
            messageBuilder.AppendLine(
                string.Format(
                    Resources.MessageClassifyFileFoundFiles,
                    classificationResult[Const.ClassificationResultFoundFileCount]));
            messageBuilder.AppendLine(
                string.Format(
                    Resources.MessageClassifyFileTargetFiles,
                    classificationResult[Const.ClassificationResultTargetFileCount]));
            messageBuilder.AppendLine(
                string.Format(
                    Resources.MessageClassifyFileClassifiedFiles,
                    classificationResult[Const.ClassificationResultClassifiedFileCount]));
            return messageBuilder.ToString();
        }

        #endregion
    }
}
