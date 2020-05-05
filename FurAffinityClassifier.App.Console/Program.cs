using System;
using System.Linq;
using FurAffinityClassifier.App.Console.Models;
using NLog;

//// TODO : "CONSOLE"の名前検討
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
                //// TODO : サブコマンドを見る(ヘルプ、設定の表示、バージョン)
                ////        サブコマンドに従った表示をする
                ////        更新後の保存が手動ならサブコマンドに「設定の保存」も追加
                

                var subcommand = args[0];
                switch (subcommand)
                {
                    case "--help":
                        new HelpModel().Execute();
                        break;
                    case "--version":
                        new VersionModel().Execute();
                        break;
                    default:
                        CONSOLE.WriteLine("NOT IMPLEMENTED");
                        break;
                }
            }
            else
            {
                //// TODO : サブコマンドを見る(設定の変更)
                ////        引数を見て更新する
                ////        更新したあとの保存は自動でやる？
                CONSOLE.WriteLine("NOT IMPLEMENTED");
            }
        }

        #endregion
    }
}
