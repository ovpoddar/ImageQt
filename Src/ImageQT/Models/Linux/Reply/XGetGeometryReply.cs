using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGetGeometryReply
{
    public byte Type; /* X_Reply */
    public byte Depth;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public uint Root;
    public short X;
    public short Y;
    public ushort Width;
    public ushort Height;
    public ushort BorderWidth;
    public ushort Pad1;
    public uint Pad2;
    public uint Pad3;
}
