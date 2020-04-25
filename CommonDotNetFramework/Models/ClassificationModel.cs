using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurAffinityClassifier.CommonDotNetFramework.Datas;

namespace FurAffinityClassifier.CommonDotNetFramework.Models
{
    /// <summary>
    /// 分類機能
    /// </summary>
    public class ClassificationModel
    {
        #region Public Method

        public bool Execute(SettingData settingData)
        {
            bool result = true;

            try
            {
                var files = Directory.GetFiles(settingData.FromFolder);
                files.ToList().ForEach(f => Console.WriteLine(f));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
