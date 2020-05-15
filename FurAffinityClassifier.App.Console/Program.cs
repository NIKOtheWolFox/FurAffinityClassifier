using FurAffinityClassifier.App.Console.ViewModels;
using NLog;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console
{
    /// <summary>
    /// アプリケーション
    /// </summary>
    public class Program
    {
        #region Private Field

        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Public Method

        /// <summary>
        /// メインメソッド
        /// </summary>
        /// <param name="args">実行時の引数</param>
        public static void Main(string[] args)
        {
            var viewModel = new ConsoleViewModel();
            CONSOLE.WriteLine(viewModel.Execute(args));
        }

        #endregion
    }
}
