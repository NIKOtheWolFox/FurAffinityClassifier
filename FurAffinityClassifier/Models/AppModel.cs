using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using Unity;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// アプリケーションの機能
    /// </summary>
    public class AppModel : IAppModel
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Modelのインスタンス</param>
        /// <param name="classificationModel">分類Modelのインスタンス</param>
        public AppModel(ISettingsModel settingsModel, IClassificationModel classificationModel)
        {
            SettingsModel = settingsModel;
            ClassificationModel = classificationModel;
        }

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder
        {
            get => SettingsModel.SettingsData.FromFolder;
            set => SettingsModel.SettingsData.FromFolder = value;
        }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder
        {
            get => SettingsModel.SettingsData.ToFolder;
            set => SettingsModel.SettingsData.ToFolder = value;
        }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist
        {
            get => SettingsModel.SettingsData.CreateFolderIfNotExist;
            set => SettingsModel.SettingsData.CreateFolderIfNotExist = value;
        }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public bool OverwriteIfExist
        {
            get => SettingsModel.SettingsData.OverwriteIfExist;
            set => SettingsModel.SettingsData.OverwriteIfExist = value;
        }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas
        {
            get => SettingsModel.SettingsData.ClassifyAsDatas;
            set => SettingsModel.SettingsData.ClassifyAsDatas = value;
        }

        /// <summary>
        /// 設定Model
        /// </summary>
        private ISettingsModel SettingsModel { get; }

        /// <summary>
        /// 分類Model
        /// </summary>
        private IClassificationModel ClassificationModel { get; }

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
