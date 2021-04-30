using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ookii.Dialogs.Wpf;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// ダイアログ用ヘルパー
    /// </summary>
    public class DialogHelper : IDialogHelper
    {
        /// <summary>
        /// フォルダー選択ダイアログを表示する
        /// </summary>
        /// <param name="initialFolder">最初に表示するフォルダー</param>
        /// <returns>ダイアログで選択したフォルダーのパス</returns>
        public string ShowFolderBrowserDialog(string initialFolder)
        {
            VistaFolderBrowserDialog dialog = new ()
            {
                SelectedPath = initialFolder,
            };
            var result = dialog.ShowDialog();

            return result.HasValue && result.Value
                ? dialog.SelectedPath
                : string.Empty;
        }
    }
}
