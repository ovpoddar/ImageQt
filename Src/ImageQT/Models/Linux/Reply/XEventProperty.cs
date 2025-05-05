using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventProperty
{
    public uint Pad00;
    public uint Window;
    public uint Atom;
    public uint Time;
    public byte State; // NewValue or Deleted
    public byte Pad1;
    public ushort Pad2;
}