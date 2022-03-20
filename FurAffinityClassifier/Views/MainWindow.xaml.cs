using System.Windows;
using FurAffinityClassifier.ViewModels;

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

            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.ShowClassifyAsSettingWindowFunc += (data) =>
                {
                    ClassifyAsSettingWindow window = new(data)
                    {
                        Owner = this,
                    };
                    window.ShowDialog();
                    return window.Result;
                };
            }
        }
    }
}
