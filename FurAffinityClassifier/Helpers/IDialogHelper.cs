using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// ダイアログ用ヘルパー interface
    /// </summary>
    public interface IDialogHelper
    {
        /// <summary>
        /// フォルダー選択ダイアログを表示する
        /// </summary>
        /// <param name="initialFolder">最初に表示するフォルダー</param>
        /// <returns>ダイアログで選択したフォルダーのパス</returns>
        string ShowFolderBrowserDialog(string initialFolder);
    }
}
