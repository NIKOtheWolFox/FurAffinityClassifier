using System.Text;
using FurAffinityClassifier.App.Console.Properties;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Models;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// オプションなし
    /// </summary>
    public class DefaultModel
    {
        #region Private Property

        /// <summary>
        /// 設定機能
        /// </summary>
        private SettingModel SettingModel { get; } = new SettingModel();

        #endregion

        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            SettingModel.LoadFromFile();

            if (!SettingModel.Validate())
            {
                CONSOLE.WriteLine(Resources.MessageInvalidSetting);
                return;
            }

            var classificationResult = new ClassificationModel(SettingModel.SettingData).Execute();
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
            CONSOLE.WriteLine(messageBuilder.ToString());
        }

        #endregion
    }
}
