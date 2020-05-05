using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using FurAffinityClassifier.App.Console.Properties;

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
            CONSOLE.WriteLine(Resources.Help.Replace("[TAB]", "\t"));
        }
    }
}
