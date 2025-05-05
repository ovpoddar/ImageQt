using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XAllocColorPlanesReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length;
    public ushort NPixels;
    public ushort Pad2;
    public uint RedMask;
    public uint GreenMask;
    public uint BlueMask;
    public uint Pad3;
    public uint Pad4;
}
