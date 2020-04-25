using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.Serialization;
using System.Windows.Forms;
using FurAffinityClassifier.CommonDotNetFramework.Datas;
using FurAffinityClassifier.CommonDotNetFramework.Models;
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
                var folderSelectDialog = new CommonOpenFileDialog()
                {
                    Title = "移動元フォルダー選択",
                    IsFolderPicker = true,
                    InitialDirectory = string.IsNullOrEmpty(FromFolderTextBox.Text) ? Environment.CurrentDirectory : FromFolderTextBox.Text,
                    DefaultDirectory = string.IsNullOrEmpty(FromFolderTextBox.Text) ? Environment.CurrentDirectory : FromFolderTextBox.Text,
                    EnsureFileExists = true,
                    EnsurePathExists = true,
                    EnsureValidNames = true,
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
                    InitialDirectory = string.IsNullOrEmpty(ToFolderTextBox.Text) ? Environment.CurrentDirectory : ToFolderTextBox.Text,
                    DefaultDirectory = string.IsNullOrEmpty(ToFolderTextBox.Text) ? Environment.CurrentDirectory : ToFolderTextBox.Text,
                    EnsureFileExists = true,
                    EnsurePathExists = true,
                    EnsureValidNames = true,
                })
            {
                if (folderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    ToFolderTextBox.Text = folderSelectDialog.FileName;
                }
            }
        }

        /// <summary>
        /// [設定を保存]ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void SaveSettingButton_Click(object sender, EventArgs e)
        {
            var data = new SettingData()
            {
                FromFolder = FromFolderTextBox.Text,
                ToFolder = ToFolderTextBox.Text,
            };
            var model = new SettingModel();
            model.Save(data);
        }

        /// <summary>
        /// [実行]ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            var model = new SettingModel();
            var data = model.Load();
            if (data == null)
            {
                Console.WriteLine("fuck");
            }
            else
            {
                Console.WriteLine($"from={data.FromFolder}");
                Console.WriteLine($"to={data.ToFolder}");
            }
        }

        #endregion
    }
}
