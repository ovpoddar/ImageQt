using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.CallerPInvoke.Windows;
internal static  class Kernel
{
    [DllImport("kernel32.dll")]
    public static extern nint GetModuleHandle(string? lpModuleName);
}
