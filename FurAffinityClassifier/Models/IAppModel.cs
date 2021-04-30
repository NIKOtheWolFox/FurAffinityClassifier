using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// アプリケーションの機能 interface
    /// </summary>
    public interface IAppModel
    {
        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        string FromFolder { get; set; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        string ToFolder { get; set; }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        bool CreateFolderIfNotExist { get; set; }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        bool OverwriteIfExist { get; set; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        List<ClassifyAsData> ClassifyAsDatas { get; set; }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        bool LoadSettings();

        /// <summary>
        /// 非同期で設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        Task<bool> LoadSettingsAsync();

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        bool SaveSettings();

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
        /// 分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        Dictionary<string, int> Classify();

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        Task<Dictionary<string, int>> ClassifyAsync();
    }
}
