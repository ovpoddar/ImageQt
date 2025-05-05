using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventMapRequest
{
    public uint Pad00;
    public uint Parent;
    public uint Window;
}