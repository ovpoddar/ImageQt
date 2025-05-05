using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XQueryTextExtentsReply
{
    public byte Type; /* X_Reply */
    public byte DrawDirection;
    public ushort SequenceNumber;
    public uint Length; /* 0 */
    public short FontAscent;
    public short FontDescent;
    public short OverallAscent;
    public short OverallDescent;
    public int OverallWidth;
    public int OverallLeft;
    public int OverallRight;
    public uint Pad;
}
