using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;
public class X11Delegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SyncHandlerDelegate(IntPtr dpy);
}
