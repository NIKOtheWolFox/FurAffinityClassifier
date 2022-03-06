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
    /// メイン画面Model interface
    /// </summary>
    public interface IMainWindowModel
    {
        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        ReactivePropertySlim<string> FromFolder { get; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        ReactivePropertySlim<string> ToFolder { get; }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        ReactivePropertySlim<bool> CreateFolderIfNotExist { get; }

        /// <summary>
        /// IDをFur Affinityから取得するか
        /// </summary>
        ReactivePropertySlim<bool> GetIdFromFurAffinity { get; }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        ReactivePropertySlim<bool> OverwriteIfExist { get; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定
        /// </summary>
        ReactiveCollection<ClassifyAsData> ClassifyAsDatas { get; }

        /// <summary>
        /// 非同期で設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        Task<bool> LoadSettingsAsync();
    }
}
