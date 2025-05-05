using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Sequential)]
public struct XEventClientMessage
{
    public uint Pad00;
    public uint Window;
    public XEventClientMessageUnion U;
}