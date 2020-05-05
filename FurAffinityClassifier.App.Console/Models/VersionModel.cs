using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// --versionオプション
    /// </summary>
    public class VersionModel
    {/// <summary>
     /// 実行する
     /// </summary>
        public void Execute()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            CONSOLE.WriteLine($"{version.Major}.{version.Minor}.{version.Build}");
        }
    }
}
