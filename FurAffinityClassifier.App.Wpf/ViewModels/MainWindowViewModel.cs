using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Text;
using FurAffinityClassifier.App.Wpf.Datas.Messages;
using FurAffinityClassifier.App.Wpf.Properties;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Datas.Messages;
using FurAffinityClassifier.Common.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.App.Wpf.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainWindowViewModel()
        {
            AppModel.LoadSetting();

            FromFolder = ReactiveProperty
                .FromObject(AppModel, x => x.FromFolder)
                .AddTo(Disposables);
            ToFolder = ReactiveProperty
                .FromObject(AppModel, x => x.ToFolder)
                .AddTo(Disposables);
            CreateFolderIfNotExist = ReactiveProperty
                .FromObject(AppModel, x => x.CreateFolderIfNotExist)
                .AddTo(Disposables);
            OverwriteIfExist = ReactiveProperty
                .FromObject(AppModel, x => x.OverwriteIfExist)
                .AddTo(Disposables);
            ClassifyAsDatas = ReactiveProperty
                .FromObject(AppModel, x => x.ClassifyAsDatas)
                .AddTo(Disposables);

            ButtonEnable = new ReactiveProperty<bool>(true);

            SelectFromFolderCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ =>
                {
                    ShowFolderSelectDialogMessage<string> showFolderSelectDialogMessage =
                        new ShowFolderSelectDialogMessage<string>(
                            s =>
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    FromFolder.Value = s;
                                }
                            })
                        {
                            Title = Resources.DialogTitleSelectFolder,
                            InitialDirectory = string.IsNullOrEmpty(FromFolder.Value) ? Environment.CurrentDirectory : FromFolder.Value,
                            DefaultDirectory = string.IsNullOrEmpty(FromFolder.Value) ? Environment.CurrentDirectory : FromFolder.Value,
                        };
                    Messenger.Default.Send(showFolderSelectDialogMessage, MessageToken.ShowFolderSelectDialog);
                })
                .AddTo(Disposables);
            SelectToFolderCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ =>
                {
                    ShowFolderSelectDialogMessage<string> showFolderSelectDialogMessage =
                        new ShowFolderSelectDialogMessage<string>(
                            s =>
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    ToFolder.Value = s;
                                }
                            })
                        {
                            Title = Resources.DialogTitleSelectFolder,
                            InitialDirectory = string.IsNullOrEmpty(ToFolder.Value) ? Environment.CurrentDirectory : ToFolder.Value,
                            DefaultDirectory = string.IsNullOrEmpty(ToFolder.Value) ? Environment.CurrentDirectory : ToFolder.Value,
                        };
                    Messenger.Default.Send(showFolderSelectDialogMessage, MessageToken.ShowFolderSelectDialog);
                })
                .AddTo(Disposables);
            SaveSettingCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(async _ =>
                {
                    ButtonEnable.Value = false;

                    ShowDialogMessage showDialogMessage = new ShowDialogMessage()
                    {
                        Title = Resources.DialogTitleSaveSetting,
                        Button = TaskDialogStandardButtons.Ok,
                    };

                    if (AppModel.ValidateSetting())
                    {
                        if (await AppModel.SaveSettingAsync())
                        {
                            showDialogMessage.Message = Resources.DialogMessageSaveSettingDone;
                            showDialogMessage.Icon = TaskDialogStandardIcon.Information;
                        }
                        else
                        {
                            showDialogMessage.Message = Resources.DialogMessageSaveSettingDone;
                            showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                        }
                    }
                    else
                    {
                        showDialogMessage.Message = Resources.DialogMessageInvalidSetting;
                        showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                    }

                    Messenger.Default.Send(showDialogMessage, MessageToken.ShowDialog);

                    ButtonEnable.Value = true;
                })
                .AddTo(Disposables);
            ExecuteCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(async _ =>
                {
                    ButtonEnable.Value = false;

                    ShowDialogMessage showDialogMessage = new ShowDialogMessage()
                    {
                        Title = Resources.DialogTitleClassifyFile,
                        Button = TaskDialogStandardButtons.Ok,
                    };

                    if (AppModel.ValidateSetting())
                    {
                        var classificationResult = await AppModel.ClassifyAsync();
                        var messageBuilder = new StringBuilder();
                        messageBuilder.AppendLine(Resources.DialogMessageClassifyFileDone);
                        messageBuilder.AppendLine();
                        messageBuilder.AppendLine(
                            string.Format(
                                Resources.DialogMessageClassifyFileFoundFiles,
                                classificationResult[Const.ClassificationResultFoundFileCount]));
                        messageBuilder.AppendLine(
                            string.Format(
                                Resources.DialogMessageClassifyFileTargetFiles,
                                classificationResult[Const.ClassificationResultTargetFileCount]));
                        messageBuilder.AppendLine(
                            string.Format(
                                Resources.DialogMessageClassifyFileClassifiedFiles,
                                classificationResult[Const.ClassificationResultClassifiedFileCount]));
                        showDialogMessage.Message = messageBuilder.ToString();
                        showDialogMessage.Icon = TaskDialogStandardIcon.Information;
                    }
                    else
                    {
                        showDialogMessage.Message = Resources.DialogMessageInvalidSetting;
                        showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                    }

                    Messenger.Default.Send(showDialogMessage, MessageToken.ShowDialog);

                    ButtonEnable.Value = true;
                })
                .AddTo(Disposables);
        }

        #endregion

        #region Public Event

        /// <summary>
        /// メモリリーク防止用のINotifyPropertyChangedの実装
        /// </summary>
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Property

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
        public ReactiveCommand<object> SaveSettingCommand { get; }

        /// <summary>
        /// [実行]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> ExecuteCommand { get; }

        #endregion

        #region Private Property

        /// <summary>
        /// アプリケーションの機能
        /// </summary>
        private AppModel AppModel { get; } = new AppModel();

        /// <summary>
        /// 一括Disposeを行うためにReactiveXxをまとめるオブジェクト
        /// </summary>
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        #endregion

        #region Public Method

        /// <summary>
        /// リソースを解放する
        /// </summary>
        /// <inheritdoc cref="IDisposable"/>
        public void Dispose()
        {
            Disposables.Dispose();
        }

        #endregion
    }
}
