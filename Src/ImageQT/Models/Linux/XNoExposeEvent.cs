using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XNoExposeEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong drawable;
    public int major_code;     /* core is CopyArea or CopyPlane */
    public int minor_code;		/* not defined in the core */
}
