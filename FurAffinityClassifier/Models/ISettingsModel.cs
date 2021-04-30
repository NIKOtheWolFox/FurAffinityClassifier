using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 設定機能 interface
    /// </summary>
    public interface ISettingsModel
    {
        /// <summary>
        /// 設定値
        /// </summary>
        SettingsData SettingsData { get; }

        /// <summary>
        /// ファイルから設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        Task<bool> LoadFromFile();

        /// <summary>
        /// ファイルに設定を書き込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        Task<bool> SaveToFile();
    }
}
