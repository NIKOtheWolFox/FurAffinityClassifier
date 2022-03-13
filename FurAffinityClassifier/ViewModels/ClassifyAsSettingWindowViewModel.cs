using System;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Datas.Messages;
using FurAffinityClassifier.Models;
using FurAffinityClassifier.Views;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// 分類設定画面 ViewModel
    /// </summary>
    public class ClassifyAsSettingWindowViewModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// 分類設定画面Model
        /// </summary>
        private readonly IClassifyAsSettingWindowModel _classifyAsSettingWindowModel;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="classifyAsSettingWindowModel">分類設定画面Model</param>
        public ClassifyAsSettingWindowViewModel(IClassifyAsSettingWindowModel classifyAsSettingWindowModel)
        {
            _classifyAsSettingWindowModel = classifyAsSettingWindowModel;

            Id = _classifyAsSettingWindowModel.Id
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            Folder = _classifyAsSettingWindowModel.Folder
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);

            OkCommand = new ReactiveCommand<object>()
                .WithSubscribe(_ => OkAction())
                .AddTo(Disposables);
            CancelCommand = new ReactiveCommand<object>()
                .WithSubscribe(_ => CancelAction())
                .AddTo(Disposables);
            ClosedCommand = new ReactiveCommand<object>()
                .WithSubscribe(x => ClosedAction(x))
                .AddTo(Disposables);
        }

        /// <summary>
        /// ID
        /// </summary>
        public ReactivePropertySlim<string> Id { get; }

        /// <summary>
        /// フォルダー
        /// </summary>
        public ReactivePropertySlim<string> Folder { get; }

        /// <summary>
        /// [OK]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> OkCommand { get; }

        /// <summary>
        /// [キャンセル]ボタンクリック時のコマンド
        /// </summary>
        public ReactiveCommand<object> CancelCommand { get; }

        /// <summary>
        /// 画面終了時のコマンド
        /// </summary>
        public ReactiveCommand<object> ClosedCommand { get; }

        /// <summary>
        /// 画面の結果
        /// </summary>
        public (bool update, ClassifyAsData classifyAsDataResult) Result =>
            (_classifyAsSettingWindowModel.Update,
            new()
            {
                Id = _classifyAsSettingWindowModel.Id.Value,
                Folder = _classifyAsSettingWindowModel.Folder.Value,
            });

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
        /// 初期化
        /// </summary>
        /// <param name="classifyAsData">画面の初期値</param>
        public void Initialize(ClassifyAsData classifyAsData)
        {
            _classifyAsSettingWindowModel.Id.Value = classifyAsData.Id;
            _classifyAsSettingWindowModel.Folder.Value = classifyAsData.Folder;
        }

        /// <summary>
        /// [OK]ボタンクリック時のAction
        /// </summary>
        private void OkAction()
        {
            _classifyAsSettingWindowModel.Update = true;
            WeakReferenceMessenger.Default.Send<ClassifyAsWindowCloseMessage>(new());
        }

        /// <summary>
        /// [キャンセル]ボタンクリック時のAction
        /// </summary>
        private void CancelAction()
        {
            _classifyAsSettingWindowModel.Update = false;
            WeakReferenceMessenger.Default.Send<ClassifyAsWindowCloseMessage>(new());
        }

        /// <summary>
        /// 画面終了時のAction
        /// </summary>
        /// <param name="x">X param</param>
        private void ClosedAction(object x)
        {
            if (x is ClassifyAsSettingWindow window)
            {
                WeakReferenceMessenger.Default.UnregisterAll(window);
            }
        }
    }
}
