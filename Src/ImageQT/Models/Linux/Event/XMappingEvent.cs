using System.Runtime.InteropServices;
using ImageQT.Models.Linux.Display;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XMappingEvent
{
    public EventType Type;
    public ulong Serial;   /* # of last request processed by server */
    public int SendEvent;    /* true if this came from a SendEvent request */
    public XDisplay* Display;   /* XDisplay the event was read from */
    public ulong Window;      /* unused */
    public int Request;        /* one of MappingModifier, MappingKeyboard,
				   MappingPointer */
    public int FirstKeyCode;  /* first keycode */
    public int Count;      /* defines range of change w. firstKeycode*/
}
