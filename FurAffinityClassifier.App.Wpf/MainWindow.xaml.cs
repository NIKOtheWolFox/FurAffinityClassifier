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
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;

namespace FurAffinityClassifier.App.Wpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow()
        {
            Logger.Debug("動作テスト");
            InitializeComponent();
        }
    }
}
