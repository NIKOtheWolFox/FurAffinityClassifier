using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.AppWindowsForms.Datas
{
    /// <summary>
    /// 定数
    /// </summary>
    internal class Const
    {
        /// <summary>
        /// 列名 ID
        /// </summary>
        public const string ColumnNameId = "id";

        /// <summary>
        /// 列名 フォルダー名
        /// </summary>
        public const string ColumnNameFolderName = "folder_name";

        /// <summary>
        /// 設定検証結果 移動元
        /// </summary>
        public const string ValidationResultKeyFromFolder = "from_folder";

        /// <summary>
        /// 設定検証結果 移動先
        /// </summary>
        public const string ValidationResultKeyToFolder = "to_folder";

        /// <summary>
        /// 設定検証結果 IDとフォルダーの紐付け
        /// </summary>
        public const string ValidationResultKeyMapping = "mapping";
    }
}
