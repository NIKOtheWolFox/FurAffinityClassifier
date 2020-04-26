using System;
using System.Windows.Forms;
using FurAffinityClassifier.AppWindowsForms.Views;

namespace FurAffinityClassifier.AppWindowsForms
{
    /// <summary>
    /// アプリケーションのメインクラス
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
