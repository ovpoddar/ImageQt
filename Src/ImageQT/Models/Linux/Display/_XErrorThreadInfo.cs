using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XErrorThreadInfo
{
    public _XErrorThreadInfo* Next; /* next in list */
    public ulong ErrorThread;
}