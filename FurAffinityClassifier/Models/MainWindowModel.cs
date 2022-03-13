using System;
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
        /// 設定読み込み済みフラグ
        /// </summary>
        private bool settingsLoaded = false;

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
            FromFolder.Subscribe(v =>
            {
                if (v != SettingsModel.SettingsData.FromFolder)
                {
                    SettingsModel.SettingsData.FromFolder = v;
                }
            });
            ToFolder = new();
            ToFolder.Subscribe(v =>
            {
                if (v != SettingsModel.SettingsData.ToFolder)
                {
                    SettingsModel.SettingsData.ToFolder = v;
                }
            });
            CreateFolderIfNotExist = new();
            CreateFolderIfNotExist.Subscribe(v =>
            {
                if (v != SettingsModel.SettingsData.CreateFolderIfNotExist)
                {
                    SettingsModel.SettingsData.CreateFolderIfNotExist = v;
                }
            });
            GetIdFromFurAffinity = new();
            GetIdFromFurAffinity.Subscribe(v =>
            {
                if (v != SettingsModel.SettingsData.GetIdFromFurAffinity)
                {
                    SettingsModel.SettingsData.GetIdFromFurAffinity = v;
                }
            });
            OverwriteIfExist = new();
            OverwriteIfExist.Subscribe(v =>
            {
                if (v != SettingsModel.SettingsData.OverwriteIfExist!)
                {
                    SettingsModel.SettingsData.OverwriteIfExist = v;
                }
            });
            ClassifyAsDatas = new();
            ClassifyAsDatas.ObserveAddChangedItems()
                .Subscribe(v =>
                {
                    if (settingsLoaded)
                    {
                        SettingsModel.SettingsData.ClassifyAsDatas.AddRange(v);
                    }
                });
            ClassifyAsDatas.ObserveRemoveChangedItems()
                .Subscribe(v =>
                {
                    if (settingsLoaded)
                    {
                        foreach (ClassifyAsData data in v)
                        {
                            SettingsModel.SettingsData.ClassifyAsDatas.RemoveAll(x => x.Id == data.Id && x.Folder == data.Folder);
                        }
                    }
                });
            ClassifyAsDatas.ObserveReplaceChangedItems()
                .Subscribe(v =>
                {
                    if (settingsLoaded)
                    {
                        if (v.OldItem.Length != v.NewItem.Length)
                        {
                            return;
                        }

                        for (int i = 0; i < v.OldItem.Length; i++)
                        {
                            ClassifyAsData oldData = v.OldItem[i];
                            int index = SettingsModel.SettingsData.ClassifyAsDatas.IndexOf(oldData);

                            ClassifyAsData newData = v.NewItem[i];
                            SettingsModel.SettingsData.ClassifyAsDatas[index] = newData;
                        }
                    }
                });
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
            SettingsModel.SettingsData.ClassifyAsDatas.ForEach(d =>
            {
                ClassifyAsDatas.Add(new()
                {
                    Id = d.Id,
                    Folder = d.Folder,
                });
            });
            settingsLoaded = true;
            return result;
        }

        /// <summary>
        /// 非同期で設定を保存する
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> SaveSettingsAsync()
        {
            return await SettingsModel.SaveToFileAsync();
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        public bool ValidateSettings()
        {
            return SettingsModel.Validate();
        }

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を格納したValueTuple</returns>
        public async Task<(int foundFiles, int targetFiles, int classifiedFiles)> ClassifyAsync()
        {
            return await ClassificationModel.ExecuteAsync(SettingsModel.SettingsData);
        }
    }
}
