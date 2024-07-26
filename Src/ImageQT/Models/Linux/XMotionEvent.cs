#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XMotionEvent
{
    public int type;
    public IntPtr serial;
    public int send_event;
    public IntPtr display;
    public IntPtr window;
    public IntPtr root;
    public IntPtr subwindow;
    public IntPtr time;
    public int x;
    public int y;
    public int x_root;
    public int y_root;
    public int state;
    public int is_hint;
    public int same_screen;
}
#endif
