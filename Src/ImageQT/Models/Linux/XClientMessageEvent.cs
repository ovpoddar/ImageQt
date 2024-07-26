#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XClientMessageEvent
{
    public int type;
    public IntPtr serial;
    public int send_event;
    public IntPtr display;
    public IntPtr window;
    public IntPtr message_type;
    public int format;
    public XClientMessageEventData data;
}

[StructLayout(LayoutKind.Explicit)]
public struct XClientMessageEventData
{
    [FieldOffset(0)]
    public IntPtr b; // If format == 8
    [FieldOffset(0)]
    public ushort s; // If format == 16
    [FieldOffset(0)]
    public int l;    // If format == 32
}
#endif
