using System.Runtime.InteropServices;

namespace ImageQt.Models.Linux;


[StructLayout(LayoutKind.Sequential)]
public struct XGraphicsExposeEvent
{

    public int type;
    public ulong serial;   
    public bool send_event;
    public IntPtr display; 
    public ulong drawable;
    public int x, y;
    public int width, height;
    public int count;     
    public int major_code;
    public int minor_code;
}
