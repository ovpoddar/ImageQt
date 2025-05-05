using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventColorMap
{
    public uint Pad00;
    public uint Window;
    public uint ColorMap;
    public byte New;
    public byte State; // Installed or UnInstalled
    public byte pad1;
    public byte Pad2;
}