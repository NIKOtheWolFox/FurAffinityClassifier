using System;
using log4net;

using CONSOLE = System.Console;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace FurAffinityClassifier.App.Console
{
    /// <summary>
    /// アプリケーション
    /// </summary>
    public class Program
    {
        #region

        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        #endregion

        #region

        /// <summary>
        /// メインメソッド
        /// </summary>
        /// <param name="args">実行時の引数</param>
        public static void Main(string[] args)
        {
            CONSOLE.WriteLine("Hello World!");
            Logger.Debug("Hello World!!");
        }

        #endregion
    }
}
