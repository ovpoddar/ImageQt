
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential)]
public struct XUnmapEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong @event;
    public ulong window;
    public bool from_configure;
}