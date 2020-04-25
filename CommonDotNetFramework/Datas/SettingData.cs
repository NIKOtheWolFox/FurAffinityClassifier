using System.Collections.Generic;

namespace FurAffinityClassifier.CommonDotNetFramework.Datas
{
    /// <summary>
    /// 設定データ
    /// </summary>
    public class SettingData
    {
        #region Construncor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingData()
        {
            FromFolder = string.Empty;
            ToFolder = string.Empty;
            IdFolderMappings = new List<IdFolderMappingData>();
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
        /// IDとフォルダーの対応
        /// </summary>
        public List<IdFolderMappingData> IdFolderMappings { get; set; }

        #endregion
    }
}
