using System.Windows;
using FurAffinityClassifier.App.Wpf.Datas.Messages;
using FurAffinityClassifier.Common.Datas.Messages;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FurAffinityClassifier.App.Wpf.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Field

        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<ShowDialogMessage>(
                this,
                MessageToken.ShowDialog,
                m =>
                {
                    using (
                        var dialog = new TaskDialog()
                        {
                            OwnerWindowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle,
                            StartupLocation = TaskDialogStartupLocation.CenterOwner,
                            Icon = m.Icon,
                            Caption = m.Title,
                            Text = m.Message,
                            StandardButtons = m.Button,
                        })
                    {
                        dialog.Show();
                    }
                });

            Messenger.Default.Register<ShowFolderSelectDialogMessage<string>>(
                this,
                MessageToken.ShowFolderSelectDialog,
                m =>
                {
                    using (
                        var folderSelectDialog = new CommonOpenFileDialog()
                        {
                            Title = m.Title,
                            IsFolderPicker = true,
                            InitialDirectory = m.InitialDirectory,
                            DefaultDirectory = m.DefaultDirectory,
                            EnsureFileExists = true,
                            EnsurePathExists = true,
                            EnsureValidNames = true,
                        })
                    {
                        var returnValue = folderSelectDialog.ShowDialog(this) == CommonFileDialogResult.Ok
                            ? folderSelectDialog.FileName
                            : string.Empty;
                        m.Execute(returnValue);
                    }
                });
        }

        #endregion
    }
}
