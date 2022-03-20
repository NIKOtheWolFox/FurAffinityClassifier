using System.Windows;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.ViewModels;

namespace FurAffinityClassifier.Views
{
    /// <summary>
    /// ClassifyAsSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ClassifyAsSettingWindow : Window
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClassifyAsSettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期値を指定するコンストラクター
        /// </summary>
        /// <param name="classifyAsData">画面の初期値</param>
        public ClassifyAsSettingWindow(ClassifyAsData classifyAsData)
            : this()
        {
            if (DataContext is ClassifyAsSettingWindowViewModel viewModel)
            {
                viewModel.Initialize(classifyAsData);
                viewModel.CloseWindowAction += () =>
                {
                    Close();
                };
            }
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
    }
}
