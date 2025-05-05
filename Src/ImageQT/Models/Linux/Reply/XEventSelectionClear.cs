using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventSelectionClear
{
    public uint Pad00;
    public uint Time;
    public uint Window;
    public uint Atom;
}