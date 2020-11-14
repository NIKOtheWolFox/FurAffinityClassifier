using FurAffinityClassifier.App.Console.Properties;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// --helpオプション
    /// </summary>
    public class HelpModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            return Resources.Help.Replace("[TAB]", "\t");
        }

        #endregion
    }
}
