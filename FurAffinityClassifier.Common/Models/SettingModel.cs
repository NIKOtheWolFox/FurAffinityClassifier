using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FurAffinityClassifier.Common.Datas;
using Newtonsoft.Json;
using NLog;

namespace FurAffinityClassifier.Common.Models
{
    /// <summary>
    /// 設定機能
    /// </summary>
    public class SettingModel
    {
        #region Private Field

        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        private readonly string settingFilePath = Path.Combine(Environment.CurrentDirectory, "setting.json");

        /// <summary>
        /// 設定データ
        /// </summary>
        private SettingData settingData = new SettingData();

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingModel()
        {
        }

        #endregion

        #region Public Property

        /// <summary>
        /// 設定データ
        /// </summary>
        public SettingData SettingData => settingData.Copy();

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder
        {
            get => settingData.FromFolder;
            set => settingData.FromFolder = value;
        }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder
        {
            get => settingData.ToFolder;
            set => settingData.ToFolder = value;
        }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist
        {
            get => settingData.CreateFolderIfNotExist;
            set => settingData.CreateFolderIfNotExist = value;
        }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public bool OverwriteIfExist
        {
            get => settingData.OverwriteIfExist;
            set => settingData.OverwriteIfExist = value;
        }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas
        {
            get => settingData.ClassifyAsDatas;
            set => settingData.ClassifyAsDatas = value;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// ファイルから設定を読み込む
        /// </summary>
        /// <returns>実行結果</returns>
        public bool LoadFromFile()
        {
            var result = true;

            try
            {
                if (File.Exists(settingFilePath))
                {
                    using (var reader = new StreamReader(settingFilePath))
                    {
                        settingData = JsonConvert.DeserializeObject<SettingData>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                settingData = new SettingData();
                result = false;
            }

            return result;
        }

        /// <summary>
        /// ファイルに設定を保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool SaveToFile()
        {
            /*
            var result = true;

            try
            {
                using (var writer = new StreamWriter(settingFilePath))
                {
                    writer.Write(JsonConvert.SerializeObject(settingData, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                result = false;
            }

            return result;
            */
            return SaveToFileAsync().Result;
        }

        /// <summary>
        /// ファイルに設定を非同期で保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public async Task<bool> SaveToFileAsync()
        {
            var result = true;

            try
            {
                using (var writer = new StreamWriter(settingFilePath))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(settingData, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>検証結果</returns>
        public bool Validate()
        {
            return !string.IsNullOrEmpty(FromFolder)
                && Directory.Exists(FromFolder)
                && !string.IsNullOrEmpty(ToFolder)
                && Directory.Exists(ToFolder)
                && ClassifyAsDatas.Count(x => string.IsNullOrEmpty(x.Id) || string.IsNullOrEmpty(x.Folder)) == 0;
        }

        #endregion
    }
}
