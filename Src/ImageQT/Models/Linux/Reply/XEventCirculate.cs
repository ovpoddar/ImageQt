using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;
[StructLayout(LayoutKind.Sequential)]
public struct XEventCirculate
{
    //The event field in the circulate record is really the parent when this
    //       is used as a CirculateRequest instead of a CirculateNotify
    public uint Pad00;
    public uint Event;
    public uint Window;
    public uint Parent;
    public byte Place; // Top or Bottom
    public byte Pad1;
    public byte Pad2;
    public byte Pad3;
}