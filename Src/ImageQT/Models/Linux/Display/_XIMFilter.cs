using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Event;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XIMFilter
{
    public _XIMFilter* Next;
    public ulong Window;
    public ulong EventMask;
    public int StartType;
    public int EndType;
    public delegate* unmanaged[Cdecl]<XDisplay*, ulong, XEvent*, nint, int> Filter;
    public nint ClientData;
}