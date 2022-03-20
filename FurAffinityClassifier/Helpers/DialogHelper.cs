using FurAffinityClassifier.Enums;
using Ookii.Dialogs.Wpf;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// ダイアログ用ヘルパー
    /// </summary>
    public class DialogHelper : IDialogHelper
    {
        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        /// <param name="icon">アイコン</param>
        public void ShowDialog(string title, string message, DialogIcon icon)
        {
            using TaskDialog dialog = new()
            {
                CenterParent = true,
                WindowTitle = title,
                Content = message,
                MainIcon = icon.ToTaskDialogIcon(),
            };
            dialog.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
            dialog.ShowDialog();
        }

        /// <summary>
        /// フォルダー選択ダイアログを表示する
        /// </summary>
        /// <param name="initialFolder">最初に表示するフォルダー</param>
        /// <returns>ダイアログで選択したフォルダーのパス</returns>
        public string ShowFolderBrowserDialog(string initialFolder)
        {
            VistaFolderBrowserDialog dialog = new()
            {
                SelectedPath = initialFolder,
            };
            bool? result = dialog.ShowDialog();

            return result.HasValue && result.Value
                ? dialog.SelectedPath
                : string.Empty;
        }
    }
}
