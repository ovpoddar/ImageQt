#if DEBUG || Windows
using System.Runtime.InteropServices;

namespace ImageQT.Models.Windows;
internal class WindowDelegate
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr WndProcDelegate(IntPtr hWnd,
      ProcessesMessage msg,
      IntPtr wParam,
      IntPtr lParam);
}
#endif