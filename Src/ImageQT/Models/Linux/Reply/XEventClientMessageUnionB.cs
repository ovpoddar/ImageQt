using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XEventClientMessageUnionB
{
    public uint Type;
    public fixed byte Bytes[20];
}