using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FurAffinityClassifier.App.Console.Properties;
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

        /// <summary>
        /// 実行時の引数
        /// </summary>
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
            if (args.Length == 2)
            {
                var param = args[1].Split("=");
                if (param.Length != 2)
                {
                    return Resources.InvalidOption;
                }

                var key = param[0];
                var value = param[1];
                switch (key)
                {
                    case "from-folder":
                        if (Directory.Exists(value))
                        {
                            appModel.FromFolder = value;
                            if (appModel.SaveSetting())
                            {
                                return "Setting changed successfully";
                            }
                            else
                            {
                                return "Failed to change setting";
                            }
                        }
                        else
                        {
                            return "Given folder not exist";
                        }

                    case "to-folder":
                        return "to-folder";
                    case "create-folder":
                        return "create-folder";
                    case "overwrite":
                        return "overwrite";
                    default:
                        return Resources.InvalidOption;
                }
            }
            else if (args.Length == 3)
            {
                return "MULTI";
            }
            else
            {
                return Resources.InvalidOption;
            }
        }

        #endregion

        #region Private Method
        #endregion
    }
}
