using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XAllocColorReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public ushort Red;
    public ushort Green;
    public ushort Blue;
    public ushort Pad2;
    public uint Pixel;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
}
