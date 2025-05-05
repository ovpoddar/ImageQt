using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XConnectionInfo
{
    public int Fd;
    public delegate* unmanaged[Cdecl]<XDisplay*, int, nint, void> ReadCallback;
    public nint CallData;
    public nint WatchData;
    public _XConnectionInfo* Next;
}