using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Disposables;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Models;
using NLog;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        #region Private Field

        /// <summary>
        /// NLogのロガー
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion

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
        }

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
