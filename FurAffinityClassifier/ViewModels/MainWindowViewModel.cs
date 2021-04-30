using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Unity;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="appModel">アプリケーションModelのインスタンス</param>
        public MainWindowViewModel(IAppModel appModel)
        {
            AppModel = appModel;

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
                .WithSubscribe(_ => SelectFromFolderAction())
                .AddTo(Disposables);
            SelectToFolderCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ => SelectToFolderAction())
                .AddTo(Disposables);
            SaveSettingsCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ => SaveSettingsAction())
                .AddTo(Disposables);
            ExecuteCommand = ButtonEnable
                .ToReactiveCommand()
                .WithSubscribe(_ => ExecuteAction())
                .AddTo(Disposables);
        }

        /// <summary>
        /// アプリケーションModel
        /// </summary>
        ////[Dependency]
        ////public IAppModel AppModel { get; set; }

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
        public ReactiveCommand<object> SaveSettingsCommand { get; }

        /// <summary>
        /// [実行]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> ExecuteCommand { get; }

        /// <summary>
        /// アプリケーションModel
        /// </summary>
        private IAppModel AppModel { get; }

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
        }

        private void SetCommands()
        {

        }

        /// <summary>
        /// 移動元の[選択]ボタンクリック時のaction
        /// </summary>
        private void SelectFromFolderAction()
        {
            System.Diagnostics.Debug.WriteLine("移動元フォルダー選択");
        }

        /// <summary>
        /// 移動先の[選択]ボタンクリック時のaction
        /// </summary>
        private void SelectToFolderAction()
        {
            System.Diagnostics.Debug.WriteLine("移動先フォルダー選択");
        }

        /// <summary>
        /// [設定を保存]ボタンクリック時のaction
        /// </summary>
        private void SaveSettingsAction()
        {
            System.Diagnostics.Debug.WriteLine("設定保存");
        }

        /// <summary>
        /// [実行]ボタンクリック時のaction
        /// </summary>
        private void ExecuteAction()
        {
            System.Diagnostics.Debug.WriteLine("実行");
        }
    }
}
