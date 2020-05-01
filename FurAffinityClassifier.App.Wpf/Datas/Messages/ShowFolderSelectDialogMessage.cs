using System;
using GalaSoft.MvvmLight.Messaging;

namespace FurAffinityClassifier.App.Wpf.Datas.Messages
{
    /// <summary>
    /// フォルダー選択ダイアログ表示メッセージ
    /// </summary>
    /// <typeparam name="T">コールバックの引数の型</typeparam>
    public class ShowFolderSelectDialogMessage<T> : NotificationMessageAction<T>
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="callback">コールバック</param>
        public ShowFolderSelectDialogMessage(Action<T> callback)
            : base(string.Empty, callback)
        {
            Title = string.Empty;
            InitialDirectory = string.Empty;
            DefaultDirectory = string.Empty;
        }

        #endregion

        #region Public Property

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 最初に表示するフォルダー
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// デフォルトのフォルダー(最近使ったフォルダーが使えない場合)
        /// </summary>
        public string DefaultDirectory { get; set; }

        #endregion
    }
}
