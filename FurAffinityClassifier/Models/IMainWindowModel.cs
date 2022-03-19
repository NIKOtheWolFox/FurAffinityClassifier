using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using Reactive.Bindings;

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

        /// <summary>
        /// 非同期で設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        Task<bool> SaveSettingsAsync();

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        bool ValidateSettings();

        /// <summary>
        /// 分類設定を追加する
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        void AddClassifyAsSetting(ClassifyAsData classifyAsData);

        /// <summary>
        /// 分類設定を削除する
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        void RemoveClassifyAsSetting(ClassifyAsData classifyAsData);

        /// <summary>
        /// 分類設定を更新する
        /// </summary>
        /// <param name="oldData">更新前の分類設定データ</param>
        /// <param name="newData">更新後の分類設定データ</param>
        void UpdateClassifyAsSetting(ClassifyAsData oldData, ClassifyAsData newData);

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を格納したValueTuple</returns>
        Task<(int foundFiles, int targetFiles, int classifiedFiles)> ClassifyAsync();
    }
}
