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
            /*
            if (args.Count() == 0)
            {
                new DefaultModel().Execute();
            }
            else if (args.Count() == 1)
            {
                var option = args[0];
                switch (option)
                {
                    case ConsoleAppConst.OptionHelp:
                        new HelpModel().Execute();
                        break;
                    case ConsoleAppConst.OptionVersion:
                        new VersionModel().Execute();
                        break;
                    default:
                        new InvalidModel().Execute();
                        break;
                }
            }
            else
            {
                new InvalidModel().Execute();
            }
            */
        }

        #endregion
    }
}
