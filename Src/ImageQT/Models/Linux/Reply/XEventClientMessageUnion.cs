using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Reply;

[StructLayout(LayoutKind.Explicit)]
public struct XEventClientMessageUnion
{
    [FieldOffset(0)]
    public XEventClientMessageUnionL L;
    [FieldOffset(0)]
    public XEventClientMessageUnionS S;
    [FieldOffset(0)]
    public XEventClientMessageUnionB B;
}