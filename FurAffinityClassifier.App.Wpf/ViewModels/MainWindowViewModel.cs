using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Common.Models;
using Reactive.Bindings;

namespace FurAffinityClassifier.App.Wpf.ViewModels
{
    /// <summary>
    /// メイン画面のViewModel
    /// </summary>
    public class MainWindowViewModel
    {
        #region Private Field

        /// <summary>
        /// 設定機能
        /// </summary>
        private readonly SettingModel settingModel = new SettingModel();

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainWindowViewModel()
        {
            settingModel.LoadFromFile();
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
