using FurAffinityClassifier.App.Console.Properties;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// 不正オプション
    /// </summary>
    public class InvalidModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            return Resources.InvalidOption;
        }

        #endregion
    }
}
