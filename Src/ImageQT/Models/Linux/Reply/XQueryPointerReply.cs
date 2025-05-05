using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XQueryPointerReply
{
    public byte Type; /* X_Reply */
    public byte SameScreen;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public uint Root;
    public uint Child;
    public short RootX;
    public short RootY;
    public short WinX;
    public short WinY;
    public ushort Mask;
    public ushort Pad1;
    public uint Pad;
}
