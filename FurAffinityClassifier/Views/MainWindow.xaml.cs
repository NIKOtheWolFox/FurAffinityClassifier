using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Datas.Messages;

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

        /// <summary>
        /// 分類設定画面を表示する
        /// </summary>
        /// <param name="recipient">メッセージを受信するオブジェクト</param>
        /// <param name="message">メッセージ</param>
        private void ShowClassifyAsWindow(object recipient, ShowClassifyAsWindowMessage message)
        {
            ClassifyAsSettingWindow window = new()
            {
                Owner = this,
            };
            window.Initialize(message.InitialData);
            window.ShowDialog();
            message.Callback(window.Result);
        }
    }
}
