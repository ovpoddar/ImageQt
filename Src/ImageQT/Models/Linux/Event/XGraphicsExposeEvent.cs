using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XGraphicsExposeEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Drawable;
    public int X;
    public int Y;
    public int width;
    public int Height;
    public int Count;      /* if non-zero, at least this many more */
    public int MajorCode;     /* core is CopyArea or CopyPlane */
    public int MinorCode;		/* not defined in the core */
}



