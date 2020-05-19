using System;
using System.Collections.Generic;
using System.Text;
using FurAffinityClassifier.Common.Models;

namespace FurAffinityClassifier.App.Console.Models
{
    /// <summary>
    /// --change-settingオプション
    /// </summary>
    public class ChangeSettingModel
    {
        #region Private Property

        /// <summary>
        /// アプリケーションの機能
        /// </summary>
        private AppModel appModel;

        private string[] args;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="appModel">アプリケーションの機能</param>
        /// <param name="args">実行時の引数</param>
        public ChangeSettingModel(AppModel appModel, string[] args)
        {
            this.appModel = appModel;
            this.args = args;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute()
        {
            switch (args.Length)
            {
                case 2:
                case 3:
                default:
                    return "TEST";
            }
        }

        #endregion
    }
}
