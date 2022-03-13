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
        public ReactivePropertySlim<string> Id { get; }

        /// <summary>
        /// フォルダー
        /// </summary>
        public ReactivePropertySlim<string> Folder { get; }

        /// <summary>
        /// 更新有無
        /// </summary>
        public bool Update { get; set; }
    }
}
