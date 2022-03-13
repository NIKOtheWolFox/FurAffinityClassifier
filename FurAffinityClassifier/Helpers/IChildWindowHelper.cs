using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// 子ウインドウ用Helper interface
    /// </summary>
    public interface IChildWindowHelper
    {
        /// <summary>
        /// 分類設定画面を表示する
        /// </summary>
        /// <param name="classifyAsDataParam">画面の初期値</param>
        /// <returns>(更新有無, 画面の入力値)</returns>
        (bool update, ClassifyAsData classifyAsDataResult) ShowClassifyAsSettingWindow(ClassifyAsData classifyAsDataParam);
    }
}
