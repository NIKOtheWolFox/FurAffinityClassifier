using FurAffinityClassifier.App.Console.Properties;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// helpサブコマンド
    /// </summary>
    public class HelpModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            CONSOLE.WriteLine(Resources.Help.Replace("[TAB]", "\t"));
        }

        #endregion
    }
}
