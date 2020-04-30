using Microsoft.WindowsAPICodePack.Dialogs;

namespace FurAffinityClassifier.App.Wpf.Datas.Messages
{
    /// <summary>
    /// ダイアログ表示メッセージ
    /// </summary>
    public class ShowDialogMessage
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ShowDialogMessage()
        {
            Title = string.Empty;
            Message = string.Empty;
            Icon = TaskDialogStandardIcon.None;
            Button = TaskDialogStandardButtons.Ok;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">メッセージ</param>
        /// <param name="icon">アイコン</param>
        /// <param name="button">ボタン</param>
        public ShowDialogMessage(string title, string message, TaskDialogStandardIcon icon, TaskDialogStandardButtons button)
        {
            Title = title;
            Message = message;
            Icon = icon;
            Button = button;
        }

        #endregion

        #region Public Property

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// アイコン
        /// </summary>
        public TaskDialogStandardIcon Icon { get; set; }

        /// <summary>
        /// ボタン
        /// </summary>
        public TaskDialogStandardButtons Button { get; set; }

        #endregion
    }
}
