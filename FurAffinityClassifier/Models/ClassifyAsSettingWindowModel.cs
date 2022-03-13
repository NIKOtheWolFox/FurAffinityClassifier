using FurAffinityClassifier.Datas;
using Reactive.Bindings;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 分類設定画面Model
    /// </summary>
    public class ClassifyAsSettingWindowModel : IClassifyAsSettingWindowModel
    {
        /// <summary>
        /// 設定Model
        /// </summary>
        private readonly ISettingsModel _settingsModel;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Model</param>
        public ClassifyAsSettingWindowModel(ISettingsModel settingsModel)
        {
            _settingsModel = settingsModel;

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
        /// 重複チェックする
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        /// <returns>true:重複なし/false:重複あり</returns>
        public bool CheckDuplicate(ClassifyAsData classifyAsData)
        {
            return false;
        }
    }
}
