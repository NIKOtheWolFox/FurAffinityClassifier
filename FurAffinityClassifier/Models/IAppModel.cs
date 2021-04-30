using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// アプリケーションの機能 interface
    /// </summary>
    public interface IAppModel
    {
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
