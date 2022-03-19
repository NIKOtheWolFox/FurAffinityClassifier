using System.Collections.Generic;

namespace FurAffinityClassifier.Datas
{
    /// <summary>
    /// 設定データ
    /// </summary>
    public class SettingsData
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingsData()
        {
            FromFolder = string.Empty;
            ToFolder = string.Empty;
            CreateFolderIfNotExist = false;
            GetIdFromFurAffinity = false;
            OverwriteIfExist = false;
            ClassifyAsDatas = new List<ClassifyAsData>();
        }

        /// <summary>
        /// 移動元フォルダー
        /// </summary>
        public string FromFolder { get; set; }

        /// <summary>
        /// 移動先フォルダー
        /// </summary>
        public string ToFolder { get; set; }

        /// <summary>
        /// 移動先のフォルダーが存在しないときに作成するか
        /// </summary>
        public bool CreateFolderIfNotExist { get; set; }

        /// <summary>
        /// IDをFur Affinityから取得するか
        /// </summary>
        public bool GetIdFromFurAffinity { get; set; }

        /// <summary>
        /// 同名のファイルが存在するときに上書きするか
        /// </summary>
        public bool OverwriteIfExist { get; set; }

        /// <summary>
        /// IDと異なるフォルダーに分類する設定のリスト
        /// </summary>
        public List<ClassifyAsData> ClassifyAsDatas { get; set; }
    }
}
