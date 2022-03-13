using System;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Datas.Messages;
using FurAffinityClassifier.Enums;
using FurAffinityClassifier.Helpers;
using FurAffinityClassifier.Models;
using FurAffinityClassifier.Properties;
using FurAffinityClassifier.Views;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// メイン画面ViewModel
    /// </summary>
    public class MainWindowViewModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// メイン画面Model
        /// </summary>
        private readonly IMainWindowModel _mainWindowModel;

        /// <summary>
        /// ダイアログHelper
        /// </summary>
        private readonly IDialogHelper _dialogHelper;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="mainWindowModel">メイン画面Model</param>
        /// <param name="dialogHelper">ダイアログHelper</param>
        public MainWindowViewModel(IMainWindowModel mainWindowModel, IDialogHelper dialogHelper)
        {
            _mainWindowModel = mainWindowModel;
            _dialogHelper = dialogHelper;

            FromFolder = _mainWindowModel.FromFolder
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            ToFolder = _mainWindowModel.ToFolder
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            CreateFolderIfNotExist = _mainWindowModel.CreateFolderIfNotExist
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            GetIdFromFurAffinity = _mainWindowModel.GetIdFromFurAffinity
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            OverwriteIfExist = _mainWindowModel.OverwriteIfExist
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            ClassifyAsDatas = _mainWindowModel.ClassifyAsDatas
                .ToReadOnlyReactiveCollection()
                .AddTo(Disposables);

            Enabled = new ReactivePropertySlim<bool>(false)
                .AddTo(Disposables);
            DataGridSelectedItem = new ReactivePropertySlim<object>()
                .AddTo(Disposables);

            SelectFromFolderCommand = Enabled
                .ToReactiveCommand()
                .WithSubscribe(_ => SelectFromFolderAction())
                .AddTo(Disposables);
            SelectToFolderCommand = Enabled
                .ToReactiveCommand()
                .WithSubscribe(_ => SelectToFolderAction())
                .AddTo(Disposables);
            AddClassifyAsSettingCommand = Enabled
                .ToReactiveCommand()
                .WithSubscribe(_ => AddClassifyAsSettingAction())
                .AddTo(Disposables);
            EditClassifyAsSettingCommand = Enabled
                .ToReactiveCommand()
                .WithSubscribe(_ => EditClassifyAsSettingAction())
                .AddTo(Disposables);
            DeleteClassifyAsSettingCommand = Enabled
                .ToReactiveCommand()
                .WithSubscribe(_ => DeleteClassifyAsSettingAction())
                .AddTo(Disposables);
            SaveSettingsCommand = Enabled
                .ToAsyncReactiveCommand()
                .WithSubscribe(_ => SaveSettingsActionAsync())
                .AddTo(Disposables);
            ExecuteCommand = Enabled
                .ToAsyncReactiveCommand()
                .WithSubscribe(_ => ExecuteActionAsync())
                .AddTo(Disposables);
            LoadedCommand = new AsyncReactiveCommand()
                .WithSubscribe(_ => LoadedActionAsync())
                .AddTo(Disposables);
            ClosedCommand = new ReactiveCommand<object>()
                .WithSubscribe(x => ClosedAction(x))
                .AddTo(Disposables);
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
        public ReadOnlyReactiveCollection<ClassifyAsData> ClassifyAsDatas { get; }

        /// <summary>
        /// 操作可能か
        /// </summary>
        public ReactivePropertySlim<bool> Enabled { get; }

        /// <summary>
        /// 分類設定一覧の選択アイテム
        /// </summary>
        public ReactivePropertySlim<object> DataGridSelectedItem { get; }

        /// <summary>
        /// 移動元の[選択]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> SelectFromFolderCommand { get; }

        /// <summary>
        /// 移動先の[選択]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> SelectToFolderCommand { get; }

        /// <summary>
        /// 分類設定の[追加]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> AddClassifyAsSettingCommand { get; }

        /// <summary>
        /// 分類設定の[編集]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> EditClassifyAsSettingCommand { get; }

        /// <summary>
        /// 分類設定の[削除]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> DeleteClassifyAsSettingCommand { get; }

        /// <summary>
        /// [設定を保存]ボタンクリック時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> SaveSettingsCommand { get; }

        /// <summary>
        /// [実行]ボタンクリック時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> ExecuteCommand { get; }

        /// <summary>
        /// 画面読み込み時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> LoadedCommand { get; }

        /// <summary>
        /// 画面終了時のコマンド
        /// </summary>
        public ReactiveCommand<object> ClosedCommand { get; }

        /// <summary>
        /// 一括Disposeを行うためにReactiveXxをまとめるオブジェクト
        /// </summary>
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        /// <summary>
        /// リソースを解放する
        /// </summary>
        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            Disposables.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 移動元の[選択]ボタンクリック時のAction
        /// </summary>
        private void SelectFromFolderAction()
        {
            string selectedFolder = _dialogHelper.ShowFolderBrowserDialog(FromFolder.Value);
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                FromFolder.Value = selectedFolder;
            }
        }

        /// <summary>
        /// 移動先の[選択]ボタンクリック時のAction
        /// </summary>
        private void SelectToFolderAction()
        {
            string selectedFolder = _dialogHelper.ShowFolderBrowserDialog(ToFolder.Value);
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                ToFolder.Value = selectedFolder;
            }
        }

        /// <summary>
        /// 分類設定の[追加]ボタンクリック時のAction
        /// </summary>
        private void AddClassifyAsSettingAction()
        {
            WeakReferenceMessenger.Default.Send<ShowClassifyAsWindowMessage>(new(new(), ((bool update, ClassifyAsData data) r) =>
            {
                if (r.update)
                {
                    _mainWindowModel.AddClassifyAsSetting(r.data);
                }
            }));
        }

        /// <summary>
        /// 分類設定の[編集]ボタンクリック時のAction
        /// </summary>
        private void EditClassifyAsSettingAction()
        {
            if (DataGridSelectedItem.Value is ClassifyAsData classifyAsData)
            {
                WeakReferenceMessenger.Default.Send<ShowClassifyAsWindowMessage>(new(classifyAsData, ((bool update, ClassifyAsData data) r) =>
                {
                    if (r.update)
                    {
                        _mainWindowModel.UpdateClassifyAsSetting(classifyAsData, r.data);
                    }
                }));
            }
        }

        /// <summary>
        /// 分類設定の[削除]ボタンクリック時のAction
        /// </summary>
        private void DeleteClassifyAsSettingAction()
        {
            if (DataGridSelectedItem.Value is ClassifyAsData classifyAsData)
            {
                _mainWindowModel.RemoveClassifyAsSetting(classifyAsData);
            }
        }

        /// <summary>
        /// [設定を保存]ボタンクリック時のaction
        /// </summary>
        /// <returns>async Task</returns>
        private async Task SaveSettingsActionAsync()
        {
            Enabled.Value = false;

            if (_mainWindowModel.ValidateSettings())
            {
                if (await _mainWindowModel.SaveSettingsAsync())
                {
                    _dialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageSaveSettingsDone, DialogIcon.Information);
                }
                else
                {
                    _dialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageSaveSettingsError, DialogIcon.Error);
                }
            }
            else
            {
                _dialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageInvalidSettings, DialogIcon.Error);
            }

            Enabled.Value = true;
        }

        /// <summary>
        /// [実行]ボタンクリック時のaction
        /// </summary>
        /// <returns>async Task</returns>
        private async Task ExecuteActionAsync()
        {
            Enabled.Value = false;

            if (_mainWindowModel.ValidateSettings())
            {
                (int foundFiles, int targetFiles, int classifiedFiles) = await _mainWindowModel.ClassifyAsync();
                StringBuilder messageBuilder = new();
                messageBuilder.AppendLine(Resources.DialogMessageClassifyFileDone);
                messageBuilder.AppendLine();
                messageBuilder.AppendLine(
                    string.Format(
                        Resources.DialogMessageClassifyFileFoundFiles,
                        foundFiles));
                messageBuilder.AppendLine(
                    string.Format(
                        Resources.DialogMessageClassifyFileTargetFiles,
                        targetFiles));
                messageBuilder.AppendLine(
                    string.Format(
                        Resources.DialogMessageClassifyFileClassifiedFiles,
                        classifiedFiles));
                _dialogHelper.ShowDialog(Resources.DialogTitleClassifyFile, messageBuilder.ToString(), DialogIcon.Information);
            }
            else
            {
                _dialogHelper.ShowDialog(Resources.DialogTitleClassifyFile, Resources.DialogMessageInvalidSettings, DialogIcon.Error);
            }

            Enabled.Value = true;
        }

        /// <summary>
        /// 画面読み込み時のAction
        /// </summary>
        /// <returns>async Task</returns>
        private async Task LoadedActionAsync()
        {
            await _mainWindowModel.LoadSettingsAsync();
            Enabled.Value = true;
        }

        /// <summary>
        /// 画面終了時のAction
        /// </summary>
        /// <param name="x">画面からのパラメーター</param>
        private void ClosedAction(object x)
        {
            if (x is MainWindow window)
            {
                WeakReferenceMessenger.Default.UnregisterAll(window);
            }
        }
    }
}
