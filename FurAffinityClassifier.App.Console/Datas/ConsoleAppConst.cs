namespace FurAffinityClassifier.App.Console.Datas
{
    /// <summary>
    /// コンソールアプリの定数
    /// </summary>
    internal class ConsoleAppConst
    {
        /// <summary>
        /// オプション ヘルプ
        /// </summary>
        public const string OptionHelp = "--help";

        /// <summary>
        /// オプション バージョンの表示
        /// </summary>
        public const string OptionVersion = "--version";

        /// <summary>
        /// オプション 設定の表示
        /// </summary>
        public const string OptionShowSetting = "--show-setting";

        /// <summary>
        /// オプション 設定の変更
        /// </summary>
        public const string OptionChangeSetting = "--change-setting";

        /// <summary>
        /// パラメーター 移動元フォルダー
        /// </summary>
        public const string ParamFromFolder = "from-folder";

        /// <summary>
        /// パラメーター 移動先フォルダー
        /// </summary>
        public const string ParamToFolder = "to-folder";

        /// <summary>
        /// パラメーター フォルダーがないときは作成する
        /// </summary>
        public const string ParamCreateFolderIfNotExist = "create-folder";

        /// <summary>
        /// パラメーター ファイルがあるときは上書きする
        /// </summary>
        public const string ParamOverwriteIfExist = "overwrite";

        /// <summary>
        /// パラメーター IDと異なるフォルダーへの分類
        /// </summary>
        public const string ParamClassifyAs = "classify-as";

        /// <summary>
        /// パラメーター IDと異なるフォルダーへの分類 追加
        /// </summary>
        public const string ParamClassifyAsAdd = "add";

        /// <summary>
        /// パラメーター IDと異なるフォルダーへの分類 削除
        /// </summary>
        public const string ParamClassifyAsDelete = "delete";
    }
}
