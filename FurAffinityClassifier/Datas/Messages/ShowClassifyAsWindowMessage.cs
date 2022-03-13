using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.Datas.Messages
{
    /// <summary>
    /// 分類設定画面を表示するMessage
    /// </summary>
    public class ShowClassifyAsWindowMessage
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="initialData">画面の初期値</param>
        /// <param name="callback">分類設定画面を閉じた後のコールバック</param>
        public ShowClassifyAsWindowMessage(ClassifyAsData initialData, Action<bool, ClassifyAsData> callback)
        {
            InitialData = initialData;
            Callback = callback;
        }

        /// <summary>
        /// 画面の初期値
        /// </summary>
        public ClassifyAsData InitialData { get; set; }

        /// <summary>
        /// 分類設定画面を閉じた後のコールバック
        /// </summary>
        public Action<bool, ClassifyAsData> Callback { get; set; }
    }
}
