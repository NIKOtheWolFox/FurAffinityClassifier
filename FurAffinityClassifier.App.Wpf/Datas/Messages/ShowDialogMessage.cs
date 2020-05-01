using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FurAffinityClassifier.App.Wpf.Datas.Messages
{
    /// <summary>
    /// ダイアログ表示メッセージ
    /// </summary>
    public class ShowDialogMessage : NotificationMessage
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ShowDialogMessage()
            : base(string.Empty)
        {
            Title = string.Empty;
            Message = string.Empty;
            Icon = TaskDialogStandardIcon.None;
            Button = TaskDialogStandardButtons.Ok;
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
