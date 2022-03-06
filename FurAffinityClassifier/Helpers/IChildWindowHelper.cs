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
        (bool update, ClassifyAsData result) ShowClassifyAsSettingWindow(ClassifyAsData classifyAsData);
    }
}
