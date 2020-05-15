using System.Reflection;

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
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        #endregion
    }
}
