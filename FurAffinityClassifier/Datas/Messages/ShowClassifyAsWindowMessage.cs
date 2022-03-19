using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FurAffinityClassifier.Datas.Messages
{
    /// <summary>
    /// 分類設定画面を表示するMessage
    /// </summary>
    public class ShowClassifyAsWindowMessage : RequestMessage<(bool update, ClassifyAsData data)>
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="initialData">画面の初期値</param>
        public ShowClassifyAsWindowMessage(ClassifyAsData initialData)
        {
            InitialData = initialData;
        }

        /// <summary>
        /// 画面の初期値
        /// </summary>
        public ClassifyAsData InitialData { get; set; }
    }
}
