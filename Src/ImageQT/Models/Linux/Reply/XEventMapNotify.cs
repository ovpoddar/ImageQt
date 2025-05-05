using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventMapNotify
{
    public uint Pad00;
    public uint Event;
    public uint Window;
    public byte Override;
    public byte Pad1;
    public byte Pad2;
    public byte Pad3;
}