/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:FurAffinityClassifier.App.Wpf"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using CommonServiceLocator;
////using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
////using Microsoft.Practices.ServiceLocation;

namespace FurAffinityClassifier.App.Wpf.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// メイン画面のViewModel
        /// </summary>
        public MainWindowViewModel MainWindowViewModel
        {
            get => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 後処理
        /// </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        #endregion
    }
}
