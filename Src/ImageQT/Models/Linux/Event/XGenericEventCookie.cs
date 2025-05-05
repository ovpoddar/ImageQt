using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XGenericEventCookie
{
    public EventType Type;         /* of event. Always GenericEvent */
    public ulong Serial;       /* # of last request processed */
    public int SendEvent;   /* true if from SendEvent request */
    public XDisplay* Display;     /* XDisplay the event was read from */
    public int Extension;    /* major opcode of extension that caused the event */
    public int EventType;       /* actual event type. */
    public uint Cookie;
    public nint Data;
}