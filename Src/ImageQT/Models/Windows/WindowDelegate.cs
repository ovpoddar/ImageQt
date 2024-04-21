using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Windows;
internal class WindowDelegate
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr WndProcDelegate(IntPtr hWnd,
      ProcessesMessage msg,
      IntPtr wParam,
      IntPtr lParam);
}
