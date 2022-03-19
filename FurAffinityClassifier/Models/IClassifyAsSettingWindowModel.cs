using Reactive.Bindings;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 分類設定画面Model interface
    /// </summary>
    public interface IClassifyAsSettingWindowModel
    {
        /// <summary>
        /// ID
        /// </summary>
        ReactivePropertySlim<string> Id { get; }

        /// <summary>
        /// フォルダー
        /// </summary>
        ReactivePropertySlim<string> Folder { get; }

        /// <summary>
        /// 更新有無
        /// </summary>
        bool Update { get; set; }

        /// <summary>
        /// 入力をチェックする
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        bool Validate();
    }
}
