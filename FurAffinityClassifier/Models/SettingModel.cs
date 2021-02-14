using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using NLog;

namespace FurAffinityClassifier.Models
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
            return false;
        }

        /// <summary>
        /// ファイルに設定を保存する
        /// </summary>
        /// <returns>実行結果</returns>
        public bool SaveToFile()
        {
            return false;
        }

        #endregion
    }
}
