using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XConnWatchInfo
{
    public delegate* unmanaged[Cdecl]<XDisplay*, nint, int, int, nint, void> Fn;
    public nint ClientData;
    public _XConnWatchInfo* Next;
}