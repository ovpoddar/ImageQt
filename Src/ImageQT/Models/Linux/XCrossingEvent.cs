#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;


[StructLayout(LayoutKind.Sequential)]
public struct XCrossingEvent
{

    public int type;
    public ulong serial;
    public bool send_event;
    public IntPtr display;
    public ulong window;
    public ulong root;
    public ulong subwindow;
    public ulong time;
    public int x, y;
    public int x_root, y_root;
    public int mode;
    public int detail;

    public bool same_screen;
    public bool focus;
    public uint state;
}
#endif
