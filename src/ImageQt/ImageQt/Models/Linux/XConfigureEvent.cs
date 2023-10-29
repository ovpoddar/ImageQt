using System.Runtime.InteropServices;

namespace ImageQt.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XConfigureEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong @event;
    public ulong window;
    public int x, y;
    public int width, height;
    public int border_width;
    public ulong above;
    public bool override_redirect;
}
