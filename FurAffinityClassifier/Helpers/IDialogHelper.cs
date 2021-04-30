using FurAffinityClassifier.Enums;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// ダイアログ用ヘルパー interface
    /// </summary>
    public interface IDialogHelper
    {
        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        /// <param name="icon">アイコン</param>
        void ShowDialog(string title, string message, DialogIcon icon);

        /// <summary>
        /// フォルダー選択ダイアログを表示する
        /// </summary>
        /// <param name="initialFolder">最初に表示するフォルダー</param>
        /// <returns>ダイアログで選択したフォルダーのパス</returns>
        string ShowFolderBrowserDialog(string initialFolder);
    }
}
