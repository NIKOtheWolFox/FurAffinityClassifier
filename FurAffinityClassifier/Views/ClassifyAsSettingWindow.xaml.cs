using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Messages;
using FurAffinityClassifier.ViewModels;

namespace FurAffinityClassifier.Views
{
    /// <summary>
    /// ClassifyAsSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ClassifyAsSettingWindow : Window
    {
        /// <summary>
        /// 初期値を指定するコンストラクター
        /// </summary>
        /// <param name="classifyAsData">画面の初期値</param>
        public ClassifyAsSettingWindow(ClassifyAsData classifyAsData)
            : this()
        {
            WeakReferenceMessenger.Default.Register<CloseClassifyAsWindowMessage>(this, CloseWindow);
            if (DataContext is ClassifyAsSettingWindowViewModel viewModel)
            {
                viewModel.Initialize(classifyAsData);
            }
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        private ClassifyAsSettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 結果
        /// </summary>
        /// <returns>(更新有無, 画面の入力値)</returns>
        public (bool update, ClassifyAsData classifyAsDataResult) Result
        {
            get
            {
                if (DataContext is ClassifyAsSettingWindowViewModel viewModel)
                {
                    return viewModel.Result;
                }
                else
                {
                    return (false, new());
                }
            }
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <param name="recipient">メッセージを受信するオブジェクト</param>
        /// <param name="message">メッセージ</param>
        private void CloseWindow(object recipient, CloseClassifyAsWindowMessage message)
        {
            Close();
        }
    }
}
