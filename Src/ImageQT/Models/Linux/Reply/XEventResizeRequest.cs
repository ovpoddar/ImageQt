using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventResizeRequest
{
    public uint Pad00;
    public uint Window;
    public ushort Width;
    public ushort Height;
}