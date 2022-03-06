using System;
using FurAffinityClassifier.Helpers;
using FurAffinityClassifier.Models;
using FurAffinityClassifier.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FurAffinityClassifier
{
    /// <summary>
    /// Model, ViewModel, Helperのロケーター
    /// </summary>
    public class Locator
    {
        /// <summary>
        /// ServiceProvider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Locator()
        {
            ServiceCollection services = new();

            services.AddTransient<MainWindowViewModel>();

            services.AddSingleton<IAppModel, AppModel>();
            services.AddTransient<IMainWindowModel, MainWindowModel>();
            services.AddSingleton<ISettingsModel, SettingsModel>();
            services.AddTransient<IClassificationModel, ClassificationModel>();

            services.AddTransient<IDialogHelper, DialogHelper>();

            serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// メイン画面 ViewModel
        /// </summary>
        public MainWindowViewModel MainWindowViewModel => serviceProvider.GetRequiredService<MainWindowViewModel>();
    }
}
