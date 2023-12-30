using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XKeyEvent
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
    public int keycode;
    public int same_screen;
}
