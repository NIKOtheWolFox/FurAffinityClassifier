using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Common.Datas;
using FurAffinityClassifier.Common.Models;
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

            /*
            FromFolder = new ReactiveProperty<string>(SettingModel.FromFolder)
                .AddTo(Disposables);
            ToFolder = new ReactiveProperty<string>(SettingModel.ToFolder)
                .AddTo(Disposables);
            CreateFolderIfNotExist = new ReactiveProperty<bool>(SettingModel.CreateFolderIfNotExist)
                .AddTo(Disposables);
            OverwriteIfExist = new ReactiveProperty<bool>(SettingModel.OverwriteIfExist)
                .AddTo(Disposables);
                */

            ExecuteCommand = new ReactiveCommand()
                .WithSubscribe(_ => {
                    Console.WriteLine("CLICK");
                    Console.WriteLine($"SettingModel.CreateFolderIfNotExist={SettingModel.CreateFolderIfNotExist}");
                    Console.WriteLine("------");
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
