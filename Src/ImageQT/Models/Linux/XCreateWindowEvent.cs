using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XCreateWindowEvent
{

    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong parent;      /* parent of the window */
    public ulong window;      /* window id of window created */
    public int x, y;       /* window location */
    public int width, height;  /* size of window */
    public int border_width;   /* border width */
    public bool override_redirect;	/* creation should be overridden */
}
