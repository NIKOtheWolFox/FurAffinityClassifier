using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Console.WriteLine("テスト");
        }

        #endregion
    }
}
