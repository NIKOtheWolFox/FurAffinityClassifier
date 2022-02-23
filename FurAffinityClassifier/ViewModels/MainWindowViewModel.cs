using System;
using System.Collections.Generic;
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
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="appModel">アプリケーションModelのインスタンス</param>
        /// <param name="dialogHelper">ダイアログHelperのインスタンス</param>
        public MainWindowViewModel(IAppModel appModel, IDialogHelper dialogHelper)
        {
            AppModel = appModel;
            DialogHelper = dialogHelper;

            //// AppModel.LoadSettings();

            FromFolder = ReactiveProperty
                .FromObject(AppModel, x => x.FromFolder)
                .AddTo(Disposables);
            ToFolder = ReactiveProperty
                .FromObject(AppModel, x => x.ToFolder)
                .AddTo(Disposables);
            CreateFolderIfNotExist = ReactiveProperty
                .FromObject(AppModel, x => x.CreateFolderIfNotExist)
                .AddTo(Disposables);
            GetIdFromFurAffinity = ReactiveProperty
                .FromObject(AppModel, x => x.GetIdFromFurAffinity)
                .AddTo(Disposables);
            OverwriteIfExist = ReactiveProperty
                .FromObject(AppModel, x => x.OverwriteIfExist)
                .AddTo(Disposables);
            ClassifyAsDatas = ReactiveProperty
                .FromObject(AppModel, x => x.ClassifyAsDatas)
                .AddTo(Disposables);

            ButtonEnable = new ReactiveProperty<bool>(false);

            LoadedCommand = new AsyncReactiveCommand()
                .WithSubscribe(_ => LoadedActionAsync())
                .AddTo(Disposables);
            SelectFromFolderCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ => SelectFromFolderAction())
                .AddTo(Disposables);
            SelectToFolderCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ => SelectToFolderAction())
                .AddTo(Disposables);
            SaveSettingsCommand = ButtonEnable
                .ToAsyncReactiveCommand()
                .WithSubscribe(_ => SaveSettingsActionAsync())
                .AddTo(Disposables);
            ExecuteCommand = ButtonEnable
                .ToAsyncReactiveCommand()
                .WithSubscribe(_ => ExecuteActionAsync())
                .AddTo(Disposables);
        }

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public ReactiveProperty<string> FromFolder { get; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public ReactiveProperty<string> ToFolder { get; }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public ReactiveProperty<bool> CreateFolderIfNotExist { get; }

        /// <summary>
        /// IDをFur Affinityから取得するか
        /// </summary>
        public ReactiveProperty<bool> GetIdFromFurAffinity { get; }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public ReactiveProperty<bool> OverwriteIfExist { get; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public ReactiveProperty<List<ClassifyAsData>> ClassifyAsDatas { get; }

        /// <summary>
        /// ボタンが操作可能か
        /// </summary>
        public ReactiveProperty<bool> ButtonEnable { get; }

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
        /// [設定を保存]ボタンクリック時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> SaveSettingsCommand { get; }

        /// <summary>
        /// [実行]ボタンクリック時のコマンド
        /// </summary>
        public AsyncReactiveCommand<object> ExecuteCommand { get; }

        /// <summary>
        /// アプリケーションModel
        /// </summary>
        private IAppModel AppModel { get; }

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

        private async Task LoadedActionAsync()
        {
            ButtonEnable.Value = true;
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
        /// [設定を保存]ボタンクリック時のaction
        /// </summary>
        private async Task SaveSettingsActionAsync()
        {
            ButtonEnable.Value = false;

            if (AppModel.ValidateSettings())
            {
                if (await AppModel.SaveSettingsAsync())
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

            ButtonEnable.Value = true;
        }

        /// <summary>
        /// [実行]ボタンクリック時のaction
        /// </summary>
        private async Task ExecuteActionAsync()
        {
            ButtonEnable.Value = false;

            if (AppModel.ValidateSettings())
            {
                (int foundFiles, int targetFiles, int classifiedFiles) = await AppModel.ClassifyAsync();
                StringBuilder messageBuilder = new ();
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

            ButtonEnable.Value = true;
        }
    }
}
