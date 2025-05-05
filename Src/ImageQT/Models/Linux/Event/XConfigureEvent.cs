using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XConfigureEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Event;
    public ulong Window;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public int BorderWidth;
    public ulong Above;
    public int OverrideRedirect;
}



