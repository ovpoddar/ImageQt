using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XQueryBestSizeReply
{
    public byte Type; /* X_Reply */
    public byte Pad1;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public ushort Width;
    public ushort Height;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
    public uint Pad7;
}