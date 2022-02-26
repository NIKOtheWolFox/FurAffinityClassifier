using System;
using FurAffinityClassifier.Helpers;
using FurAffinityClassifier.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FurAffinityClassifier.ViewModels
{
    /// <summary>
    /// ViewModelとDIの設定
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// ViewModelとDIのServiceProvider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ViewModelLocator()
        {
            ServiceCollection services = new();

            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<IAppModel, AppModel>();
            services.AddSingleton<ISettingsModel, SettingsModel>();
            services.AddSingleton<IClassificationModel, ClassificationModel>();

            services.AddTransient<IDialogHelper, DialogHelper>();

            serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// メイン画面のViewModel
        /// </summary>
        public MainWindowViewModel MainWindowViewModel => serviceProvider.GetRequiredService<MainWindowViewModel>();
    }
}
