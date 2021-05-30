using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// 設定機能
    /// </summary>
    public class SettingsModel : ISettingsModel
    {
        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        private readonly string settingsFilePath = Path.Combine(Environment.CurrentDirectory, "settings.json");

        /// <summary>
        /// JSONのシリアライズ/デシリアライズ設定
        /// </summary>
        private readonly JsonSerializerOptions serializeOption = new ()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true,
        };

        /// <summary>
        /// 設定値
        /// </summary>
        public SettingsData SettingsData { get; private set; }

        /// <summary>
        /// ファイルから設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool LoadFromFile()
        {
            bool result = true;

            try
            {
                if (File.Exists(settingsFilePath))
                {
                    using StreamReader stream = new (settingsFilePath);
                    SettingsData = JsonSerializer.Deserialize<SettingsData>(stream.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                SettingsData = new SettingsData();
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 非同期でファイルから設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> LoadFromFileAsync()
        {
            bool result = true;

            try
            {
                if (File.Exists(settingsFilePath))
                {
                    using StreamReader stream = new (settingsFilePath);
                    SettingsData = JsonSerializer.Deserialize<SettingsData>(await stream.ReadToEndAsync());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                SettingsData = new SettingsData();
                result = false;
            }

            return result;
        }

        /// <summary>
        /// ファイルに設定を書き込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public bool SaveToFile()
        {
            bool result = true;

            try
            {
                using StreamWriter writer = new (settingsFilePath);
                writer.Write(JsonSerializer.Serialize(SettingsData, serializeOption));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 非同期でファイルに設定を書き込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> SaveToFileAsync()
        {
            bool result = true;

            try
            {
                using StreamWriter writer = new (settingsFilePath);
                await writer.WriteAsync(JsonSerializer.Serialize(SettingsData, serializeOption));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        public bool Validate()
        {
            return !string.IsNullOrEmpty(SettingsData.FromFolder)
                && Directory.Exists(SettingsData.FromFolder)
                && !string.IsNullOrEmpty(SettingsData.ToFolder)
                && Directory.Exists(SettingsData.ToFolder)
                && !SettingsData.ClassifyAsDatas.Any(x => string.IsNullOrEmpty(x.Id) || string.IsNullOrEmpty(x.Folder));
        }
    }
}
