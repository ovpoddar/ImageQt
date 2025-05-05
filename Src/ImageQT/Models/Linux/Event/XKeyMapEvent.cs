using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

/* generated on EnterWindow and FocusIn  when KeyMapState selected */
[StructLayout(LayoutKind.Sequential)]
public unsafe struct XKeyMapEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Window;
    public fixed char keyVector[32];
}



