using FurAffinityClassifier.Datas;
using FurAffinityClassifier.Views;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// 子ウインドウ用Helper
    /// </summary>
    public class ChildWindowHelper : IChildWindowHelper
    {
        /// <summary>
        /// 分類設定画面を表示する
        /// </summary>
        /// <param name="classifyAsDataParam">画面の初期値</param>
        /// <returns>(更新有無, 画面の入力値)</returns>
        public (bool update, ClassifyAsData classifyAsDataResult) ShowClassifyAsSettingWindow(ClassifyAsData classifyAsDataParam)
        {
            ClassifyAsSettingWindow window = new();
            window.Initialize(classifyAsDataParam);
            window.ShowDialog();
            return window.Result;
        }
    }
}
