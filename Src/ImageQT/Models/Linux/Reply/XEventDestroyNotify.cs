using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventDestroyNotify
{
    public uint Pad00;
    public uint Event;
    public uint Window;
}