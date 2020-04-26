using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FurAffinityClassifier.AppWindowsForms.Datas;
using FurAffinityClassifier.AppWindowsForms.ViewModels;
using log4net;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FurAffinityClassifier.AppWindowsForms.Views
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private Field

        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainForm));

        /// <summary>
        /// ViewModel
        /// </summary>
        private MainFormViewModel viewModel = new MainFormViewModel();

        /// <summary>
        /// 振り分け設定DataGridView用のデータテーブル
        /// </summary>
        private DataTable dataTable = new DataTable();

        /// <summary>
        /// データテーブルとDataGridViewを紐付けるオブジェクト
        /// </summary>
        private BindingSource bindingSource = new BindingSource();

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            SetDataGridView();

            viewModel.LoadSetting();
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 仮面のロードイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            FromFolderTextBox.Text = viewModel.FromFolder;
            ToFolderTextBox.Text = viewModel.ToFolder;
            CreateFolderIfNotExistCheckBox.Checked = viewModel.CreateFolderIfNotExist;

            dataTable.Rows.Clear();
            foreach (var classifyAsData in viewModel.ClassifyAs)
            {
                var row = dataTable.NewRow();
                row[Const.ColumnNameId] = classifyAsData.Key;
                row[Const.ColumnNameFolderName] = classifyAsData.Value;
                dataTable.Rows.Add(row);
            }

            dataTable.RowDeleted += (s, ea) =>
            {
                viewModel.ClassifyAs = dataTable
                    .AsEnumerable()
                    .Where(
                        row => !string.IsNullOrEmpty(row[Const.ColumnNameId].ToString())
                            && !string.IsNullOrEmpty(row[Const.ColumnNameFolderName].ToString()))
                    .ToDictionary(
                        row => row[Const.ColumnNameId].ToString(),
                        row => row[Const.ColumnNameFolderName].ToString());
            };
            dataTable.RowChanged += (s, ea) =>
            {
                viewModel.ClassifyAs = dataTable
                    .AsEnumerable()
                    .Where(
                        row => !string.IsNullOrEmpty(row[Const.ColumnNameId].ToString())
                            && !string.IsNullOrEmpty(row[Const.ColumnNameFolderName].ToString()))
                    .ToDictionary(
                        row => row[Const.ColumnNameId].ToString(),
                        row => row[Const.ColumnNameFolderName].ToString());
            };
        }

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
                    viewModel.FromFolder = folderSelectDialog.FileName;
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
                    viewModel.ToFolder = folderSelectDialog.FileName;
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
            var result = viewModel.SaveSetting();
            using (
                var dialog = new TaskDialog()
                {
                    OwnerWindowHandle = Handle,
                    StartupLocation = TaskDialogStartupLocation.CenterOwner,
                    Icon = result ? TaskDialogStandardIcon.Information : TaskDialogStandardIcon.Error,
                    Caption = "設定の保存",
                    Text = result ? "設定の保存が完了しました。" : "設定の保存に失敗しました。",
                    StandardButtons = TaskDialogStandardButtons.Ok,
                })
            {
                dialog.Show();
            }
        }

        /// <summary>
        /// [実行]ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            var validationResult = viewModel.ValidateSetting();
            if (validationResult.ContainsValue(false))
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (!validationResult[Const.ValidationResultKeyFromFolder])
                {
                    stringBuilder.AppendLine("移動元フォルダーが不正です。");
                }

                if (!validationResult[Const.ValidationResultKeyToFolder])
                {
                    stringBuilder.AppendLine("移動先フォルダーが不正です。");
                }

                if (!validationResult[Const.ValidationResultKeyMapping])
                {
                    stringBuilder.AppendLine("振り分け設定が不正です。");
                }

                using (
                    var dialog = new TaskDialog()
                    {
                        OwnerWindowHandle = Handle,
                        StartupLocation = TaskDialogStartupLocation.CenterOwner,
                        Icon = TaskDialogStandardIcon.Error,
                        Caption = "設定を確認してください",
                        Text = stringBuilder.ToString(),
                        StandardButtons = TaskDialogStandardButtons.Ok,
                    })
                {
                    dialog.Show();
                }

                return;
            }

            var ClassificationResult = viewModel.ExecuteClassification();
            using (
                var dialog = new TaskDialog()
                {
                    OwnerWindowHandle = Handle,
                    StartupLocation = TaskDialogStartupLocation.CenterOwner,
                    Icon = ClassificationResult ? TaskDialogStandardIcon.Information : TaskDialogStandardIcon.Error,
                    Caption = "ファイルの分類",
                    Text = ClassificationResult ? "ファイルの分類が完了しました。" : "ファイルの分類に失敗しました。",
                    StandardButtons = TaskDialogStandardButtons.Ok,
                })
            {
                dialog.Show();
            }
        }

        /// <summary>
        /// フォルダー作成有無の切り替えイベントハンドラー
        /// </summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベントパラメーター</param>
        private void CreateFolderIfNotExistCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            viewModel.CreateFolderIfNotExist = CreateFolderIfNotExistCheckBox.Checked;
        }

        /// <summary>
        /// 振り分け設定DataGridViewを設定する
        /// </summary>
        private void SetDataGridView()
        {
            dataTable.Columns.Add(Const.ColumnNameId, typeof(string));
            dataTable.Columns.Add(Const.ColumnNameFolderName, typeof(string));

            bindingSource.DataSource = dataTable;
            ClassifyAsDataGridView.DataSource = bindingSource;

            ClassifyAsDataGridView.Columns[0].HeaderText = "ID";
            ClassifyAsDataGridView.Columns[0].Width = 100;

            ClassifyAsDataGridView.Columns[1].HeaderText = "フォルダー名";
            ClassifyAsDataGridView.Columns[1].Width = 100;
        }

        #endregion
    }
}
