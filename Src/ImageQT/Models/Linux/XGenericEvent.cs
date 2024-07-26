#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XGenericEvent
{
    public int type;         /* of event. Always GenericEvent */
    public ulong serial;       /* # of last request processed */
    public bool send_event;   /* true if from SendEvent request */
    public IntPtr display;     /* Display the event was read from */
    public int extension;    /* major opcode of extension that caused the event */
    public int evtype;       /* actual event type. */
}
#endif
