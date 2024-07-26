#if DEBUG || Windows
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Windows;
internal enum WindowStyle : uint
{
    DBLCLKS = 0x0008
}
#endif