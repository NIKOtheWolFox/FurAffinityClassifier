using System.Windows;

namespace FurAffinityClassifier.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            if (DataContext == null)
            {
                System.Diagnostics.Debug.WriteLine("DataContext == null");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DataContext != null");
            }

            try
            {
                if (App.Current.Resources["Locator"] is FurAffinityClassifier.Locator X)
                {
                    System.Diagnostics.Debug.WriteLine("App.Current.Resources[Locator] as FurAffinityClassifier.Locator != null");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("App.Current.Resources[Locator] as FurAffinityClassifier.Locator == null");
                }
            }
            catch
            {
                // do nothing
            }
        }
    }
}
