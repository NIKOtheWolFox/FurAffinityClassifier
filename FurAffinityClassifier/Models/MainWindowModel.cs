using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// メイン画面Model
    /// </summary>
    public class MainWindowModel : IMainWindowModel
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Modelのインスタンス</param>
        /// <param name="classificationModel">分類Modelのインスタンス</param>
        public MainWindowModel(ISettingsModel settingsModel, IClassificationModel classificationModel)
        {
            SettingsModel = settingsModel;
            ClassificationModel = classificationModel;

            FromFolder = new();
            ToFolder = new();
            CreateFolderIfNotExist = new();
            GetIdFromFurAffinity = new();
            OverwriteIfExist = new();
            ClassifyAsDatas = new();
        }

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public ReactivePropertySlim<string> FromFolder { get; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public ReactivePropertySlim<string> ToFolder { get; }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public ReactivePropertySlim<bool> CreateFolderIfNotExist { get; }

        /// <summary>
        /// IDをFur Affinityから取得するか
        /// </summary>
        public ReactivePropertySlim<bool> GetIdFromFurAffinity { get; }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public ReactivePropertySlim<bool> OverwriteIfExist { get; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定
        /// </summary>
        public ReactiveCollection<ClassifyAsData> ClassifyAsDatas { get; }

        /// <summary>
        /// 設定Model
        /// </summary>
        private ISettingsModel SettingsModel { get; }

        /// <summary>
        /// 分類Model
        /// </summary>
        private IClassificationModel ClassificationModel { get; }

        /// <summary>
        /// 非同期で設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> LoadSettingsAsync()
        {
            bool result = await SettingsModel.LoadFromFileAsync();
            FromFolder.Value = SettingsModel.SettingsData.FromFolder;
            ToFolder.Value = SettingsModel.SettingsData.ToFolder;
            CreateFolderIfNotExist.Value = SettingsModel.SettingsData.CreateFolderIfNotExist;
            GetIdFromFurAffinity.Value = SettingsModel.SettingsData.GetIdFromFurAffinity;
            OverwriteIfExist.Value = SettingsModel.SettingsData.OverwriteIfExist;
            ClassifyAsDatas.AddRangeOnScheduler(SettingsModel.SettingsData.ClassifyAsDatas);
            return result;
        }

        /// <summary>
        /// 非同期で設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> SaveSettingsAsync()
        {
            UpdateSettings();
            return await SettingsModel.SaveToFileAsync();
        }

        private void UpdateSettings()
        {
            SettingsModel.SettingsData.FromFolder = FromFolder.Value;
            SettingsModel.SettingsData.ToFolder = ToFolder.Value;
            SettingsModel.SettingsData.CreateFolderIfNotExist = CreateFolderIfNotExist.Value;
            SettingsModel.SettingsData.GetIdFromFurAffinity = GetIdFromFurAffinity.Value;
            SettingsModel.SettingsData.OverwriteIfExist = OverwriteIfExist.Value;
            SettingsModel.SettingsData.ClassifyAsDatas = ClassifyAsDatas.ToList();
        }
    }
}
