using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Explicit)]
public unsafe struct XClientMessageEventData
{
    [FieldOffset(0)] public fixed sbyte b[20];
    [FieldOffset(0)] public fixed short s[10];
    [FieldOffset(0)] public fixed long l[5];
}