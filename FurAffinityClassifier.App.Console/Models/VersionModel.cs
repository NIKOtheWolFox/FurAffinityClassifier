using System.Reflection;

using CONSOLE = System.Console;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// --versionオプション
    /// </summary>
    public class VersionModel
    {
        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        public void Execute()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            CONSOLE.WriteLine($"{version.Major}.{version.Minor}.{version.Build}");
        }

        #endregion
    }
}
