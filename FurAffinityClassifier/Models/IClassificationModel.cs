using System.Collections.Generic;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 分類機能 interface
    /// </summary>
    public interface IClassificationModel
    {
        /// <summary>
        /// 分類を非同期で実行する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を記録したDictionary</returns>
        Task<Dictionary<string, int>> ExecuteAsync(SettingsData settingsData);
    }
}
