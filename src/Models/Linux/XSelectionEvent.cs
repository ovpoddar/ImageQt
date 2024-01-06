using System.Runtime.InteropServices;

namespace ImageQt.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XSelectionEvent
{
    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong requestor;
    public ulong selection;
    public ulong target;
    public ulong property;      /* ATOM or None */
    public ulong time;
}