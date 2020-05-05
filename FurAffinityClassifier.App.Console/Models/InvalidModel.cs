using FurAffinityClassifier.App.Console.Properties;
using System;
using System.Collections.Generic;
using System.Text;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// 不正オプション
    /// </summary>
    public class InvalidModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            CONSOLE.WriteLine(Resources.InvalidOption);
        }

        #endregion
    }
}
