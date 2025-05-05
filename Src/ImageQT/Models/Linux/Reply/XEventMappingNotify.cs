using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventMappingNotify
{
    public uint Pad00;
    public byte Request;
    public byte FirstKeyCode;
    public byte Count;
    public byte Pad1;
}