using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XListHostsReply
{
    public byte Type; /* X_Reply */
    public byte Enabled;
    public ushort SequenceNumber;
    public uint Length;
    public ushort NHosts;
    public ushort Pad1;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
    public uint Pad7;
}
;