using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Enums;
using FurAffinityClassifier.Helpers;
using FurAffinityClassifier.Models;
using FurAffinityClassifier.Properties;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// メイン画面ViewModel
    /// </summary>
    public class MainWindowViewModel : ObservableObject, IDisposable
    {
        private readonly IChildWindowHelper _childWindowHelper;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="mainWindowModel">メイン画面Modelのインスタンス</param>
        /// <param name="dialogHelper">ダイアログHelperのインスタンス</param>
        /// <param name="childWindowHelper">子ウインドウHelperのインスタンス</param>
        public MainWindowViewModel(IMainWindowModel mainWindowModel, IDialogHelper dialogHelper, IChildWindowHelper childWindowHelper)
        {
            MainWindowModel = mainWindowModel;
            DialogHelper = dialogHelper;
            _childWindowHelper = childWindowHelper;

            FromFolder = MainWindowModel
                .FromFolder
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            ToFolder = MainWindowModel
                .ToFolder
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            CreateFolderIfNotExist = MainWindowModel
                .CreateFolderIfNotExist
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            GetIdFromFurAffinity = MainWindowModel
                .GetIdFromFurAffinity
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            OverwriteIfExist = MainWindowModel
                .OverwriteIfExist
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            ClassifyAsDatas = MainWindowModel
                .ClassifyAsDatas
                .ToReadOnlyReactiveCollection()
                .AddTo(Disposables);

            Enabled = new ReactiveProperty<bool>(false)
                .AddTo(Disposables);

            LoadedCommand = new AsyncReactiveCommand()
                .WithSubscribe(_ => LoadedActionAsync())
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
        public ReactiveProperty<bool> Enabled { get; }

        /// <summary>
        /// 画面読み込み時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> LoadedCommand { get; }

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
        /// メイン画面Model
        /// </summary>
        private IMainWindowModel MainWindowModel { get; }

        /// <summary>
        /// ダイアログHelper
        /// </summary>
        private IDialogHelper DialogHelper { get; }

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
        /// 画面読み込み時のaction
        /// </summary>
        /// <returns>async Task</returns>
        private async Task LoadedActionAsync()
        {
            await MainWindowModel.LoadSettingsAsync();
            Enabled.Value = true;
        }

        /// <summary>
        /// 移動元の[選択]ボタンクリック時のaction
        /// </summary>
        private void SelectFromFolderAction()
        {
            string selectedFolder = DialogHelper.ShowFolderBrowserDialog(FromFolder.Value);
            if (!string.IsNullOrEmpty(selectedFolder))
            {
                FromFolder.Value = selectedFolder;
            }
        }

        /// <summary>
        /// 移動先の[選択]ボタンクリック時のaction
        /// </summary>
        private void SelectToFolderAction()
        {
            string selectedFolder = DialogHelper.ShowFolderBrowserDialog(ToFolder.Value);
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
            //(bool update, ClassifyAsData result) = _childWindowHelper.ShowClassifyAsSettingWindow(new());
            (bool update, ClassifyAsData result) = _childWindowHelper.ShowClassifyAsSettingWindow(new() { Id = "testId", Folder = "testFolder" });
            System.Diagnostics.Debug.WriteLine($"update={update}");
            System.Diagnostics.Debug.WriteLine($"result.Id={result.Id}");
            System.Diagnostics.Debug.WriteLine($"result.Folder={result.Folder}");
        }

        /// <summary>
        /// 分類設定の[編集]ボタンクリック時のAction
        /// </summary>
        private void EditClassifyAsSettingAction()
        {
            System.Diagnostics.Debug.WriteLine("分類設定の編集");
            System.Diagnostics.Debug.WriteLine($"ClassifyAsDatas.Count : {ClassifyAsDatas.Count}");
            System.Diagnostics.Debug.WriteLine($"ClassifyAsDatas.ToList().Count : {ClassifyAsDatas.ToList().Count}");
        }

        /// <summary>
        /// 分類設定の[削除]ボタンクリック時のAction
        /// </summary>
        private void DeleteClassifyAsSettingAction()
        {
            System.Diagnostics.Debug.WriteLine("分類設定の削除");
        }

        /// <summary>
        /// [設定を保存]ボタンクリック時のaction
        /// </summary>
        /// <returns>async Task</returns>
        private async Task SaveSettingsActionAsync()
        {
            Enabled.Value = false;

            if (MainWindowModel.ValidateSettings())
            {
                if (await MainWindowModel.SaveSettingsAsync())
                {
                    DialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageSaveSettingsDone, DialogIcon.Information);
                }
                else
                {
                    DialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageSaveSettingsError, DialogIcon.Error);
                }
            }
            else
            {
                DialogHelper.ShowDialog(Resources.DialogTitleSaveSettings, Resources.DialogMessageInvalidSettings, DialogIcon.Error);
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

            if (MainWindowModel.ValidateSettings())
            {
                (int foundFiles, int targetFiles, int classifiedFiles) = await MainWindowModel.ClassifyAsync();
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
                DialogHelper.ShowDialog(Resources.DialogTitleClassifyFile, messageBuilder.ToString(), DialogIcon.Information);
            }
            else
            {
                DialogHelper.ShowDialog(Resources.DialogTitleClassifyFile, Resources.DialogMessageInvalidSettings, DialogIcon.Error);
            }

            Enabled.Value = true;
        }
    }
}
