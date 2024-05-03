using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XFocusChangeEvent
{

    public int type;       /* FocusIn or FocusOut */
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong window;      /* window of event */
    public int mode;       /* NotifyNormal, NotifyWhileGrabbed, NotifyGrab, NotifyUngrab */
    public int detail;
};