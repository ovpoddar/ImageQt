using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XGenericReply
{
    public byte Type;            /* X_Reply */
    public byte Data1;           /* depends on reply type */
    public ushort SequenceNumber; /* of last request received by server */
    public uint Length;           /* 4 byte quantities beyond size of GenericReply */
    public uint Data00;
    public uint Data01;
    public uint Data02;
    public uint Data03;
    public uint Data04;
    public uint Data05;
}
