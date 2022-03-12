using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace FurAffinityClassifier.Models
{
    public class ClassifyAsSettingWindowModel : IClassifyAsSettingWindowModel
    {
        public ClassifyAsSettingWindowModel()
        {
            Id = new();
            Folder = new();
        }

        public ReactivePropertySlim<string> Id { get; }

        public ReactivePropertySlim<string> Folder { get; }
    }
}
