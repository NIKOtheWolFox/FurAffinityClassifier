using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FurAffinityClassifier.Views;
using FurAffinityClassifier.Models;
using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace FurAffinityClassifier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// <see cref="PrismApplicationBase.CreateShell()"/>
        /// </summary>
        /// <returns>メイン画面</returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// <see cref="PrismApplicationBase.RegisterTypes(IContainerRegistry)"/>
        /// </summary>
        /// <param name="containerRegistry">Container by Prism</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IAppModel, AppModel>();
            containerRegistry.Register<ISettingsModel, SettingsModel>();
            containerRegistry.Register<IClassificationModel, ClassificationModel>();
        }
    }
}
