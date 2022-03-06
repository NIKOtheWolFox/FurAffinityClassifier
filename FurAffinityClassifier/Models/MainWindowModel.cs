using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// メイン画面のModel
    /// </summary>
    public class MainWindowModel : IMainWindowModel
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Modelのインスタンス</param>
        /// <param name="classificationModel">分類Modelのインスタンス</param>
        public MainWindowModel(ISettingsModel settingsModel, IClassificationModel classificationModel)
        {
            SettingsModel = settingsModel;
            ClassificationModel = classificationModel;
        }

        public ReactivePropertySlim<string> FromFolder { get; }

        public ReactivePropertySlim<string> ToFolder { get; }

        public ReactivePropertySlim<bool> CreateFolderIfNotExist { get; }

        public ReactivePropertySlim<bool> GetIdFromFurAffinity { get; }

        public ReactivePropertySlim<bool> OverwriteIfExist { get; }

        public ReactiveCollection<ClassifyAsData> ClassifyAsDatas { get; }

        /// <summary>
        /// 設定Model
        /// </summary>
        private ISettingsModel SettingsModel { get; }

        /// <summary>
        /// 分類Model
        /// </summary>
        private IClassificationModel ClassificationModel { get; }
    }
}
