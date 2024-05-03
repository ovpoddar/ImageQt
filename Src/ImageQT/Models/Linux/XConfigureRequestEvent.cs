using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XConfigureRequestEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong parent;
    public ulong window;
    public int x, y;
    public int width, height;
    public int border_width;
    public ulong above;
    public int detail;     /* Above, Below, TopIf, BottomIf, Opposite */
    public ulong value_mask;
}
