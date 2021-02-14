using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Models;
using NLog;
using Prism.Mvvm;
using Reactive.Bindings;

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
            Debug.WriteLine("test");
        }

        #endregion

        #region Public Property
        #endregion

        #region Private Property

        /// <summary>
        /// アプリケーションの機能
        /// </summary>
        private AppModel AppModel { get; } = new AppModel();

        #endregion
    }
}
