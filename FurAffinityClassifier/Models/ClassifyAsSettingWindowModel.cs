using Reactive.Bindings;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 分類設定画面Model
    /// </summary>
    public class ClassifyAsSettingWindowModel : IClassifyAsSettingWindowModel
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClassifyAsSettingWindowModel()
        {
            Id = new();
            Folder = new();
            Update = false;
        }

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

        /// <summary>
        /// 入力をチェックする
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        public bool Validate()
        {
            return !string.IsNullOrEmpty(Id.Value) && !string.IsNullOrEmpty(Folder.Value);
        }
    }
}
