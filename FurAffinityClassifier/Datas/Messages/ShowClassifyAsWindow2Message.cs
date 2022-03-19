using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Datas.Messages
{
    public class ShowClassifyAsWindow2Message : RequestMessage<(bool update, ClassifyAsData data)>
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="initialData">画面の初期値</param>
        public ShowClassifyAsWindow2Message(ClassifyAsData initialData)
        {
            InitialData = initialData;
        }

        /// <summary>
        /// 画面の初期値
        /// </summary>
        public ClassifyAsData InitialData { get; set; }
    }
}
