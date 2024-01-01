using System.Runtime.InteropServices;

namespace ImageQt.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XVisibilityEvent
{
    public int type;
    public ulong serial;
    public bool send_event;
    public IntPtr display;
    public ulong window;
    public int state;
}
