using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ImageQT.Models.Linux.X11Delegates;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XLockPtrs
{
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, int, void> lock_display;
                                              
    public delegate* unmanaged[Cdecl]<XDisplay*, IntPtr, int, void> unlock_display;
}
