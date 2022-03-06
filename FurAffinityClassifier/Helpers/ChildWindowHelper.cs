using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.Datas;

namespace FurAffinityClassifier.Helpers
{
    /// <summary>
    /// 子ウインドウ用Helper
    /// </summary>
    public class ChildWindowHelper : IChildWindowHelper
    {
        public (bool update, ClassifyAsData result) ShowClassifyAsSettingWindow(ClassifyAsData classifyAsData)
        {
            return (false, new ClassifyAsData);
        }
    }
}
