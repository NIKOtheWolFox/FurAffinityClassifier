namespace FurAffinityClassifier.Datas
{
    /// <summary>
    /// IDと異なるフォルダーに分類する設定のデータ
    /// </summary>
    public class ClassifyAsData
    {
        #region Constructor

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClassifyAsData()
        {
            Id = string.Empty;
            Folder = string.Empty;
        }

        #endregion

        #region Public Property

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// フォルダー
        /// </summary>
        public string Folder { get; set; }

        #endregion
    }
}
