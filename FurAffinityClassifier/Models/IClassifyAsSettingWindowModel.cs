using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace FurAffinityClassifier.Models
{
    public interface IClassifyAsSettingWindowModel
    {
        public ReactivePropertySlim<string> Id { get; }

        public ReactivePropertySlim<string> Folder { get; }

        public bool Update { get; set; }
    }
}
