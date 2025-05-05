using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XAnyEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public bool SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;/* XDisplay the event was read from */
    public ulong Window;	/* window on which event was requested in event mask */
}
