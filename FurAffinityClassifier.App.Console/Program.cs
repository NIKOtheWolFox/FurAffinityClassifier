using System;
using System.Linq;
using FurAffinityClassifier.App.Console.Datas;
using FurAffinityClassifier.App.Console.Models;
using NLog;

using CONSOLE = System.Console;

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
                //// TODO : 分類処理の実装
                CONSOLE.WriteLine("NOT IMPLEMENTED");
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
                        CONSOLE.WriteLine("NOT IMPLEMENTED");
                        break;
                }
            }
            else
            {
                CONSOLE.WriteLine("NOT IMPLEMENTED");
            }
        }

        #endregion
    }
}
