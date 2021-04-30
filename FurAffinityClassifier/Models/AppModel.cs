using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// アプリケーションの機能
    /// </summary>
    public class AppModel : IAppModel
    {
        /// <summary>
        /// 設定Model
        /// </summary>
        [Dependency]
        public ISettingsModel SettingsModel { get; set; }

        /// <summary>
        /// 分類Model
        /// </summary>
        [Dependency]
        public IClassificationModel ClassificationModel { get; set; }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool LoadSettings()
        {
            return LoadSettingsAsync().Result;
        }

        /// <summary>
        /// 設定を非同期で読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> LoadSettingsAsync()
        {
            return await SettingsModel.LoadFromFile();
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool SaveSettings()
        {
            return SaveSettingsAsync().Result;
        }

        /// <summary>
        /// 設定を非同期で保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> SaveSettingsAsync()
        {
            return await SettingsModel.SaveToFile();
        }

        /// <summary>
        /// 分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        public Dictionary<string, int> Classify()
        {
            return ClassifyAsync().Result;
        }

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        public async Task<Dictionary<string, int>> ClassifyAsync()
        {
            return await ClassificationModel.ExecuteAsync(SettingsModel.SettingsData);
        }
    }
}
