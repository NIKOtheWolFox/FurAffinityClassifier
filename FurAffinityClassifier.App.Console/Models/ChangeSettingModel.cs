using System.IO;
using System.Linq;
using FurAffinityClassifier.App.Console.Properties;
using FurAffinityClassifier.Common.Datas;
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
                        return ChangeFromFolder(value);
                    case "to-folder":
                        return ChangeToFolder(value);
                    case "create-folder":
                        return ChangeCreateFolderIfNotExist(value);
                    case "overwrite":
                        return ChangeOverwriteIfExist(value);
                    default:
                        return Resources.InvalidOption;
                }
            }
            else if (args.Length == 4)
            {
                if (args[1] == "classify-as")
                {
                    return ChangeClassifyAs(args[2], args[3]);
                }
                else
                {
                    return Resources.InvalidOption;
                }
            }
            else
            {
                return Resources.InvalidOption;
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 移動元フォルダーを変更する
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>コンソールに出力する文字列</returns>
        private string ChangeFromFolder(string value)
        {
            if (Directory.Exists(value))
            {
                appModel.FromFolder = value;
                if (appModel.SaveSetting())
                {
                    return Resources.MessageChangeSettingDone;
                }
                else
                {
                    return Resources.MessageChangeSettingFailed;
                }
            }
            else
            {
                return Resources.MessageChangeSettingInvalidValue;
            }
        }

        /// <summary>
        /// 移動先フォルダーを変更する
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>コンソールに出力する文字列</returns>
        private string ChangeToFolder(string value)
        {
            if (Directory.Exists(value))
            {
                appModel.ToFolder = value;
                if (appModel.SaveSetting())
                {
                    return Resources.MessageChangeSettingDone;
                }
                else
                {
                    return Resources.MessageChangeSettingFailed;
                }
            }
            else
            {
                return Resources.MessageChangeSettingInvalidValue;
            }
        }

        /// <summary>
        /// フォルダー自動作成有無を変更する
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>コンソールに出力する文字列</returns>
        private string ChangeCreateFolderIfNotExist(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                appModel.CreateFolderIfNotExist = result;
                if (appModel.SaveSetting())
                {
                    return Resources.MessageChangeSettingDone;
                }
                else
                {
                    return Resources.MessageChangeSettingFailed;
                }
            }
            else
            {
                return Resources.MessageChangeSettingInvalidValue;
            }
        }

        /// <summary>
        /// ファイルの上書き有無を変更する
        /// </summary>
        /// <param name="value">入力値</param>
        /// <returns>コンソールに出力する文字列</returns>
        private string ChangeOverwriteIfExist(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                appModel.OverwriteIfExist = result;
                if (appModel.SaveSetting())
                {
                    return Resources.MessageChangeSettingDone;
                }
                else
                {
                    return Resources.MessageChangeSettingFailed;
                }
            }
            else
            {
                return Resources.MessageChangeSettingInvalidValue;
            }
        }

        /// <summary>
        /// IDと異なるフォルダーへの分類設定を変更する
        /// </summary>
        /// <param name="mode">モード(追加/削除)</param>
        /// <param name="param">パラメーター</param>
        /// <returns>コンソールに出力する文字列</returns>
        private string ChangeClassifyAs(string mode, string param)
        {
            if (mode == "add")
            {
                var x = param.Split("=");
                if (x.Length != 2)
                {
                    return Resources.MessageChangeSettingInvalidValue;
                }

                var id = x[0];
                var folder = x[1];
                appModel.ClassifyAsDatas.Add(
                    new ClassifyAsData()
                    {
                        Id = id,
                        Folder = folder
                    });
                if (appModel.SaveSetting())
                {
                    return Resources.MessageChangeSettingDone;
                }
                else
                {
                    return Resources.MessageChangeSettingFailed;
                }
            }
            else if (mode == "delete")
            {
                if (appModel.ClassifyAsDatas.Count(x => x.Id == param) != 0)
                {
                    appModel.ClassifyAsDatas.RemoveAll(x => x.Id == param);
                    if (appModel.SaveSetting())
                    {
                        return Resources.MessageChangeSettingDone;
                    }
                    else
                    {
                        return Resources.MessageChangeSettingFailed;
                    }
                }
                else
                {
                    return Resources.MessageChangeSettingInvalidValue;
                }
            }
            else
            {
                return Resources.InvalidOption;
            }
        }

        #endregion
    }
}
