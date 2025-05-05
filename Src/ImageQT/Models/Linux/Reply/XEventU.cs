using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XEventU
{
    public byte Type;
    public byte Detail;
    public ushort SequenceNumber;
}
