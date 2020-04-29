using System.Collections.Generic;

namespace FurAffinityClassifier.Common.Datas
{
    /// <summary>
    /// 設定データ
    /// </summary>
    public class SettingData
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingData()
        {
            FromFolder = string.Empty;
            ToFolder = string.Empty;
            CreateFolderIfNotExist = false;
            ClassifyAsDatas = new List<ClassifyAsData>();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder { get; set; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder { get; set; }

        /// <summary>
        /// フォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist { get; set; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas { get; set; }

        #endregion
    }
}
