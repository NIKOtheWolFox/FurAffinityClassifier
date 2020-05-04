using System;
using System.Collections.Generic;
using System.Text;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// helpサブコマンド
    /// </summary>
    public class HelpModel
    {
        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            CONSOLE.WriteLine("HELP");
        }
    }
}
