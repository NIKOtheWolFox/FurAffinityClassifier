using System.Linq;
using FurAffinityClassifier.App.Console.Datas;
using FurAffinityClassifier.App.Console.Models;
using FurAffinityClassifier.Common.Models;

namespace FurAffinityClassifier.App.Console.ViewModels
{
    /// <summary>
    /// コンソールのViewModel
    /// </summary>
    public class ConsoleViewModel
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ConsoleViewModel()
        {
            AppModel.LoadSetting();
        }

        #endregion

        #region Private Property

        /// <summary>
        /// アプリケーションの機能
        /// </summary>
        private AppModel AppModel { get; } = new AppModel();

        #endregion

        #region Public Method

        /// <summary>
        /// 引数に応じた処理を実行する
        /// </summary>
        /// <param name="args">実行時の引数</param>
        /// <returns>コンソールに出力する文字列</returns>
        public string Execute(string[] args)
        {
            var message = string.Empty;

            if (args.Count() == 0)
            {
                message = new DefaultModel().Execute(AppModel);
            }
            else if (args.Count() == 1)
            {
                var option = args[0];
                switch (option)
                {
                    case ConsoleAppConst.OptionHelp:
                        message = new HelpModel().Execute();
                        break;
                    case ConsoleAppConst.OptionVersion:
                        message = new VersionModel().Execute();
                        break;
                    case ConsoleAppConst.OptionShowSetting:
                        message = new ShowSettingModel(AppModel).Execute();
                        break;
                    default:
                        message = new InvalidModel().Execute();
                        break;
                }
            }
            else
            {
                message = new InvalidModel().Execute();
            }

            return message;
        }

        #endregion
    }
}
