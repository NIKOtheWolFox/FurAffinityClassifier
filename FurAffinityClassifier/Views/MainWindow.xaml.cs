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
        }
    }
}
