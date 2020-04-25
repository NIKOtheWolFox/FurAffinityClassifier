using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.CommonDotNetFramework.Datas;
using Newtonsoft.Json;

namespace FurAffinityClassifier.CommonDotNetFramework.Models
{
    /// <summary>
    /// 設定機能
    /// </summary>
    public class SettingModel
    {
        #region Private Field

        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        private readonly string settingFilePath = Path.Combine(Environment.CurrentDirectory, "setting.json");

        #endregion

        #region Public Method

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <returns>設定データ</returns>
        public SettingData Load()
        {
            SettingData settingData = null;

            try
            {
                using (var reader = new StreamReader(settingFilePath))
                {
                    settingData = JsonConvert.DeserializeObject<SettingData>(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                settingData = null;
            }

            return settingData;
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        /// <param name="settingData">設定データ</param>
        /// <returns>実行結果</returns>
        public bool Save(SettingData settingData)
        {
            try
            {
                using (var writer = new StreamWriter(settingFilePath))
                {
                    writer.Write(JsonConvert.SerializeObject(settingData, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        #endregion
    }
}
