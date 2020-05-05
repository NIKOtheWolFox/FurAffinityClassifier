using System;
using System.Collections.Generic;
using System.Text;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// オプションなし
    /// </summary>
    public class DefaultModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            CONSOLE.WriteLine("NO option");
        }

        #endregion
    }
}
