#if DEBUG || OSX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal class WindowDelegate
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void windowWillClose(IntPtr receiver, IntPtr selector, IntPtr arguments);

}
#endif
