using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XCreateWindowEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Parent;      /* parent of the window */
    public ulong Window;      /* window id of window created */
    public int X;
    public int Y;       /* window location */
    public int Width;
    public int Height;  /* size of window */
    public int BorderWidth;   /* border width */
    public int OverrideRedirect;	/* creation should be overridden */
}



