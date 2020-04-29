using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace FurAffinityClassifier.App.Wpf.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainWindowViewModel()
        {
            Content = new ReactiveProperty<string>("TEST");
        }

        #endregion

        #region Public Property

        /// <summary>
        /// テスト用プロパティ
        /// </summary>
        public ReactiveProperty<string> Content { get; set; }

        #endregion
    }
}
