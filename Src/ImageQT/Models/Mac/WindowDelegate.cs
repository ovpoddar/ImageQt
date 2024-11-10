#if DEBUG || OSX
using System.Runtime.InteropServices;

namespace ImageQT.Models.Mac;
internal class WindowDelegate
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WindowWillClose(IntPtr receiver, IntPtr selector, IntPtr arguments);

}
#endif
