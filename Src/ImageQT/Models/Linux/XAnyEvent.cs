#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;
[StructLayout(LayoutKind.Sequential, Size = (24 * sizeof(long)))]

public struct XAnyEvent
{
    public Event type;
    public ulong serial;
    public bool send_event;
    public IntPtr display;
    public ulong window;

}
#endif
