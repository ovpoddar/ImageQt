using System.Runtime.InteropServices;

namespace ImageQt.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XMappingEvent
{
    public int type;
    public ulong serial;   /* # of last request processed by server */
    public bool send_event;    /* true if this came from a SendEvent request */
    public IntPtr display;   /* Display the event was read from */
    public ulong window;      /* unused */
    public int request;        /* one of MappingModifier, MappingKeyboard, MappingPointer */
    public int first_keycode;  /* first keycode */
    public int count;
}