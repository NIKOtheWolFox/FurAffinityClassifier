using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Datas.Messages;
using FurAffinityClassifier.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    public class ClassifyAsSettingWindowViewModel : ObservableObject, IDisposable
    {
        private readonly IClassifyAsSettingWindowModel _classifyAsSettingWindowModel;

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
        }

        public ReactivePropertySlim<string> Id { get; }

        public ReactivePropertySlim<string> Folder { get; }

        public ReactiveCommand<object> OkCommand { get; }

        public ReactiveCommand<object> CancelCommand { get; }

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

        public void Initialize(ClassifyAsData classifyAsData)
        {
            _classifyAsSettingWindowModel.Id.Value = classifyAsData.Id;
            _classifyAsSettingWindowModel.Folder.Value = classifyAsData.Folder;
        }

        private void OkAction()
        {
            _classifyAsSettingWindowModel.Update = true;
            WeakReferenceMessenger.Default.Send<ClassifyAsWindowCloseMessage>(new());
        }

        private void CancelAction()
        {
            _classifyAsSettingWindowModel.Update = false;
            WeakReferenceMessenger.Default.Send<ClassifyAsWindowCloseMessage>(new());
        }
    }
}
