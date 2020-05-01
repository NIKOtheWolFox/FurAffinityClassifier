using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.App.Wpf.Datas.Messages;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Datas.Messages;
using FurAffinityClassifier.Common.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            SettingModel.LoadFromFile();

            FromFolder = ReactiveProperty
                .FromObject(SettingModel, x => x.FromFolder)
                .AddTo(Disposables);
            ToFolder = ReactiveProperty
                .FromObject(SettingModel, x => x.ToFolder)
                .AddTo(Disposables);
            CreateFolderIfNotExist = ReactiveProperty
                .FromObject(SettingModel, x => x.CreateFolderIfNotExist)
                .AddTo(Disposables);
            OverwriteIfExist = ReactiveProperty
                .FromObject(SettingModel, x => x.OverwriteIfExist)
                .AddTo(Disposables);
            ClassifyAsDatas = ReactiveProperty
                .FromObject(SettingModel, x => x.ClassifyAsDatas)
                .AddTo(Disposables);

            SelectFromFolderCommand = new ReactiveCommand()
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
                            Title = "移動元フォルダー選択",
                            InitialDirectory = string.IsNullOrEmpty(FromFolder.Value) ? Environment.CurrentDirectory : FromFolder.Value,
                            DefaultDirectory = string.IsNullOrEmpty(FromFolder.Value) ? Environment.CurrentDirectory : FromFolder.Value,
                        };
                    Messenger.Default.Send(showFolderSelectDialogMessage, MessageToken.ShowFolderSelectDialog);
                })
                .AddTo(Disposables);
            SelectToFolderCommand = new ReactiveCommand()
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
                            Title = "移動先フォルダー選択",
                            InitialDirectory = string.IsNullOrEmpty(ToFolder.Value) ? Environment.CurrentDirectory : ToFolder.Value,
                            DefaultDirectory = string.IsNullOrEmpty(ToFolder.Value) ? Environment.CurrentDirectory : ToFolder.Value,
                        };
                    Messenger.Default.Send(showFolderSelectDialogMessage, MessageToken.ShowFolderSelectDialog);
                })
                .AddTo(Disposables);
            SaveSettingCommand = new ReactiveCommand()
                .WithSubscribe(_ =>
                {
                    ShowDialogMessage showDialogMessage = new ShowDialogMessage()
                    {
                        Title = "設定の保存",
                        Button = TaskDialogStandardButtons.Ok,
                    };

                    if (SettingModel.Validate())
                    {
                        if (SettingModel.SaveToFile())
                        {
                            showDialogMessage.Message = "設定の保存が完了しました。";
                            showDialogMessage.Icon = TaskDialogStandardIcon.Information;
                        }
                        else
                        {
                            showDialogMessage.Message = "設定の保存に失敗しました。";
                            showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                        }
                    }
                    else
                    {
                        showDialogMessage.Message = "設定に誤りがあります。";
                        showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                    }

                    Messenger.Default.Send(showDialogMessage, MessageToken.ShowDialog);
                })
                .AddTo(Disposables);
            ExecuteCommand = new ReactiveCommand()
                .WithSubscribe(_ =>
                {
                    ShowDialogMessage showDialogMessage = new ShowDialogMessage()
                    {
                        Title = "ファイルの分類",
                        Button = TaskDialogStandardButtons.Ok,
                    };

                    if (SettingModel.Validate())
                    {
                        var classificationResult = new ClassificationModel(SettingModel.SettingData).Execute();
                        var messageBuilder = new StringBuilder();
                        messageBuilder.AppendLine("ファイルの分類が完了しました。");
                        messageBuilder.AppendLine();
                        messageBuilder.AppendLine($"移動元フォルダーにあるファイル数:{classificationResult[Const.ClassificationResultFoundFileCount]}");
                        messageBuilder.AppendLine($"分類対象のファイル数:{classificationResult[Const.ClassificationResultTargetFileCount]}");
                        messageBuilder.AppendLine($"分類したファイル数:{classificationResult[Const.ClassificationResultClassifiedFileCount]}");
                        showDialogMessage.Message = messageBuilder.ToString();
                        showDialogMessage.Icon = TaskDialogStandardIcon.Information;
                    }
                    else
                    {
                        showDialogMessage.Message = "設定に誤りがあります。";
                        showDialogMessage.Icon = TaskDialogStandardIcon.Error;
                    }

                    Messenger.Default.Send(showDialogMessage, MessageToken.ShowDialog);
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
        /// 設定機能
        /// </summary>
        private SettingModel SettingModel { get; } = new SettingModel();

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
