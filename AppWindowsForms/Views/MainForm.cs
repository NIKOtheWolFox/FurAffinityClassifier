using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FurAffinityClassifier.AppWindowsForms.Views
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// コピー元[選択]ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void FromFolderButton_Click(object sender, EventArgs e)
        {
            using (
                var folderSelectDialog = new CommonOpenFileDialog("移動元フォルダー選択")
                {
                    Title = "",
                    IsFolderPicker = true,
                    InitialDirectory = Environment.CurrentDirectory,
                    DefaultDirectory = Environment.CurrentDirectory,
                })
            {
                if (folderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    FromFolderTextBox.Text = folderSelectDialog.FileName;
                }
            }
        }

        /// <summary>
        /// 移動先[選択]ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void ToFolderButton_Click(object sender, EventArgs e)
        {
            using (
                var folderSelectDialog = new CommonOpenFileDialog()
                {
                    Title = "移動先フォルダー選択",
                    IsFolderPicker = true,
                    InitialDirectory = Environment.CurrentDirectory,
                    DefaultDirectory = Environment.CurrentDirectory,
                })
            {
                if (folderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    ToFolderTextBox.Text = folderSelectDialog.FileName;
                }
            }
        }

        #endregion
    }
}
