using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XError
{
    public byte Type; /* X_Error */
    public byte ErrorCode;
    public ushort SequenceNumber; /* the nth request from this client */
    public uint ResourceID;
    public ushort MinorCode;
    public byte MajorCode;
    public byte Pad1;
    public uint Pad3;
    public uint Pad4;
    public uint Pad5;
    public uint Pad6;
    public uint Pad7;
}
