using Ookii.Dialogs.Wpf;

namespace FurAffinityClassifier.Enums
{
    /// <summary>
    /// ダイアログのアイコン
    /// </summary>
    public enum DialogIcon
    {
        /// <summary>
        /// 盾アイコン
        /// </summary>
        Sheild,

        /// <summary>
        /// 情報アイコン
        /// </summary>
        Information,

        /// <summary>
        /// エラーアイコン
        /// </summary>
        Error,

        /// <summary>
        /// 警告アイコン
        /// </summary>
        Warning,
    }

    /// <summary>
    /// ダイアログのアイコンenum 拡張クラス
    /// </summary>
    public static class DialogIconExtensions
    {
        /// <summary>
        /// Ookii.dialogのenumに変換する
        /// </summary>
        /// <param name="dialogIcon">ダイアログのアイコン(DialogIcon)</param>
        /// <returns>ダイアログのアイコン(Ookii.Dialogs.Wpf.TaskDialogIcon)</returns>
        public static TaskDialogIcon ToTaskDialogIcon(this DialogIcon dialogIcon)
        {
            return dialogIcon switch
            {
                DialogIcon.Sheild => TaskDialogIcon.Shield,
                DialogIcon.Information => TaskDialogIcon.Information,
                DialogIcon.Error => TaskDialogIcon.Error,
                DialogIcon.Warning => TaskDialogIcon.Warning,
                _ => TaskDialogIcon.Custom,
            };
        }
    }
}
