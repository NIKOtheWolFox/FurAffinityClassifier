using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FurAffinityClassifier.Datas;
using FurAffinityClassifier.ViewModels;

namespace FurAffinityClassifier.Views
{
    /// <summary>
    /// ClassifyAsSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ClassifyAsSettingWindow : Window
    {
        public ClassifyAsSettingWindow()
        {
            InitializeComponent();
        }

        public void Initialize(ClassifyAsData classifyAsData)
        {
            if (DataContext is ClassifyAsSettingWindowViewModel viewModel)
            {
                viewModel.Initialize(classifyAsData);
            }
        }

        public (bool update, ClassifyAsData classifyAsDataResult) Result()
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
