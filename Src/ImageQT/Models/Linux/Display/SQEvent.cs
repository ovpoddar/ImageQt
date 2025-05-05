using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Event;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SQEvent
{
    public SQEvent* Next;
    public _XEvent Event;
    public ulong QSerialNumber;
}