using System.Linq;
using FurAffinityClassifier.App.Console.Datas;
using FurAffinityClassifier.App.Console.Models;
using NLog;

namespace FurAffinityClassifier.App.Console
{
    /// <summary>
    /// アプリケーション
    /// </summary>
    public class Program
    {
        #region

        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region

        /// <summary>
        /// メインメソッド
        /// </summary>
        /// <param name="args">実行時の引数</param>
        public static void Main(string[] args)
        {
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
        }

        #endregion
    }
}
