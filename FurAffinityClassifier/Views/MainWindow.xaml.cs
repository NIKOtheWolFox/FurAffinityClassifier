using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Messages;

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
            WeakReferenceMessenger.Default.Register<ShowClassifyAsWindowMessage>(this, ShowClassifyAsWindow);
        }

        private void ShowClassifyAsWindow(object recipient, ShowClassifyAsWindowMessage message)
        {
            ClassifyAsSettingWindow window = new(message.InitialData)
            {
                Owner = this,
            };
            window.ShowDialog();
            message.Reply(window.Result);
        }
    }
}
