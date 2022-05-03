using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAffinityClassifier.ViewModels
{
    public interface IWindowClosable
    {
        Action CloseWindowAction { get; set; }
    }
}
