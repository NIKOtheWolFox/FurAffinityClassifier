using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.ViewModels
{
    public class ClassifyAsSettingWindowViewModel : ObservableObject, IDisposable
    {
        private readonly ClassifyAsSettingWindowModel model;

        public ClassifyAsSettingWindowViewModel(ClassifyAsSettingWindowModel classifyAsSettingWindowModel)
        {
            model = classifyAsSettingWindowModel;

            Id = model.Id
                .ToReactivePropertySlimAsSynchronized(x => x.Value)
                .AddTo(Disposables);
            Folder = model.Folder
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

        public bool Update { get; private set; }

        public ClassifyAsData ClassifyAsData
        {
            get
            {
                return new ClassifyAsData { Id = Id.Value, Folder = Folder.Value };
            }

            set
            {
                Id.Value = value.Id;
                Folder.Value = value.Folder;
            }
        }

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
            model.Id.Value = classifyAsData.Id;
            model.Folder.Value = classifyAsData.Folder;
        }

        private void OkAction()
        {
            Update = true;
        }

        private void CancelAction()
        {
            Update = false;
        }
    }
}
