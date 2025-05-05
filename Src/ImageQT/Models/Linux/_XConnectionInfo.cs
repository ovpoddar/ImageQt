using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XConnectionInfo
{
    public int Fd;
    public delegate* unmanaged[Cdecl]<XDisplay*, int, IntPtr, void> ReadCallback;
    public IntPtr CallData;
    public IntPtr WatchData;
    public _XConnectionInfo* Next;
}
