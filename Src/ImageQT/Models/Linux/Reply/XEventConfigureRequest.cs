using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XEventConfigureRequest
{
    public uint Pad00;
    public uint Parent;
    public uint Window;
    public uint Sibling;
    public short X;
    public short Y;
    public ushort Width;
    public ushort Height;
    public ushort BorderWidth;
    public ushort ValueMask;
    public uint Pad1;
}