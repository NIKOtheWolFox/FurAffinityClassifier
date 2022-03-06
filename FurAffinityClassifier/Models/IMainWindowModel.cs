using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace FurAffinityClassifier.Models
{
    /// <summary>
    /// メイン画面のModel interface
    /// </summary>
    public interface IMainWindowModel
    {
        ReactivePropertySlim<string> FromFolder { get; }

        ReactivePropertySlim<string> ToFolder { get; }

        ReactivePropertySlim<bool> CreateFolderIfNotExist { get; }

        ReactivePropertySlim<bool> GetIdFromFurAffinity { get; }

        ReactivePropertySlim<bool> OverwriteIfExist { get; }

        ReactiveCollection<ClassifyAsData> ClassifyAsDatas { get; }
    }
}
