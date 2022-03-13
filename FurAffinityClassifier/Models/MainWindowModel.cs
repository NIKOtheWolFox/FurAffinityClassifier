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
        /// 設定Model
        /// </summary>
        private readonly ISettingsModel _settingsModel;

        /// <summary>
        /// 分類Model
        /// </summary>
        private readonly IClassificationModel _classificationModel;

        /// <summary>
        /// 設定読み込み済みフラグ
        /// </summary>
        private bool settingsLoaded = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="settingsModel">設定Model</param>
        /// <param name="classificationModel">分類Model</param>
        public MainWindowModel(ISettingsModel settingsModel, IClassificationModel classificationModel)
        {
            _settingsModel = settingsModel;
            _classificationModel = classificationModel;

            FromFolder = new();
            FromFolder.Subscribe(v =>
            {
                if (v != _settingsModel.SettingsData.FromFolder)
                {
                    _settingsModel.SettingsData.FromFolder = v;
                }
            });
            ToFolder = new();
            ToFolder.Subscribe(v =>
            {
                if (v != _settingsModel.SettingsData.ToFolder)
                {
                    _settingsModel.SettingsData.ToFolder = v;
                }
            });
            CreateFolderIfNotExist = new();
            CreateFolderIfNotExist.Subscribe(v =>
            {
                if (v != _settingsModel.SettingsData.CreateFolderIfNotExist)
                {
                    _settingsModel.SettingsData.CreateFolderIfNotExist = v;
                }
            });
            GetIdFromFurAffinity = new();
            GetIdFromFurAffinity.Subscribe(v =>
            {
                if (v != _settingsModel.SettingsData.GetIdFromFurAffinity)
                {
                    _settingsModel.SettingsData.GetIdFromFurAffinity = v;
                }
            });
            OverwriteIfExist = new();
            OverwriteIfExist.Subscribe(v =>
            {
                if (v != _settingsModel.SettingsData.OverwriteIfExist!)
                {
                    _settingsModel.SettingsData.OverwriteIfExist = v;
                }
            });
            ClassifyAsDatas = new();
            ClassifyAsDatas.ObserveAddChangedItems()
                .Subscribe(v =>
                {
                    if (settingsLoaded)
                    {
                        _settingsModel.SettingsData.ClassifyAsDatas.AddRange(v);
                    }
                });
            ClassifyAsDatas.ObserveRemoveChangedItems()
                .Subscribe(v =>
                {
                    if (settingsLoaded)
                    {
                        foreach (ClassifyAsData data in v)
                        {
                            _settingsModel.SettingsData.ClassifyAsDatas.RemoveAll(x => x.Id == data.Id && x.Folder == data.Folder);
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
                            int index = _settingsModel.SettingsData.ClassifyAsDatas.IndexOf(oldData);

                            ClassifyAsData newData = v.NewItem[i];
                            _settingsModel.SettingsData.ClassifyAsDatas[index] = newData;
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
        /// 非同期で設定を読み込む
        /// </summary>
        /// <returns>true:成功/false:失敗</returns>
        public async Task<bool> LoadSettingsAsync()
        {
            bool result = await _settingsModel.LoadFromFileAsync();
            FromFolder.Value = _settingsModel.SettingsData.FromFolder;
            ToFolder.Value = _settingsModel.SettingsData.ToFolder;
            CreateFolderIfNotExist.Value = _settingsModel.SettingsData.CreateFolderIfNotExist;
            GetIdFromFurAffinity.Value = _settingsModel.SettingsData.GetIdFromFurAffinity;
            OverwriteIfExist.Value = _settingsModel.SettingsData.OverwriteIfExist;
            _settingsModel.SettingsData.ClassifyAsDatas.ForEach(d =>
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
            return await _settingsModel.SaveToFileAsync();
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>true:OK/false:NG</returns>
        public bool ValidateSettings()
        {
            return _settingsModel.Validate();
        }

        /// <summary>
        /// 分類設定を追加する
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        public void AddClassifyAsSetting(ClassifyAsData classifyAsData)
        {
            ClassifyAsDatas.Add(classifyAsData);
        }

        /// <summary>
        /// 分類設定を削除する
        /// </summary>
        /// <param name="classifyAsData">分類設定データ</param>
        public void RemoveClassifyAsSetting(ClassifyAsData classifyAsData)
        {
            ClassifyAsDatas.Remove(classifyAsData);
        }

        /// <summary>
        /// 分類設定を更新する
        /// </summary>
        /// <param name="oldData">更新前の分類設定データ</param>
        /// <param name="newData">更新後の分類設定データ</param>
        public void UpdateClassifyAsSetting(ClassifyAsData oldData, ClassifyAsData newData)
        {
            int index = ClassifyAsDatas.IndexOf(oldData);
            ClassifyAsDatas[index] = newData;
        }

        /// <summary>
        /// 非同期で分類する
        /// </summary>
        /// <param name="settingsData">設定</param>
        /// <returns>ファイルの数を格納したValueTuple</returns>
        public async Task<(int foundFiles, int targetFiles, int classifiedFiles)> ClassifyAsync()
        {
            return await _classificationModel.ExecuteAsync(_settingsModel.SettingsData);
        }
    }
}
