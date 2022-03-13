using FurAffinityClassifier.Datas;
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

        /// <summary>
        /// 重複チェックする
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        /// <returns>true:重複なし/false:重複あり</returns>
        public bool CheckDuplicate(ClassifyAsData classifyAsData);
    }
}
