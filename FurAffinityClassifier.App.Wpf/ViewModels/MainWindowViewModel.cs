using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.App.Wpf.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Content = new ReactiveProperty<string>("TEST");
        }

        public ReactiveProperty<string> Content { get; set; }
    }
}
