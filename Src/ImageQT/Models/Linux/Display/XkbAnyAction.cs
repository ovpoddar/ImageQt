using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct XkbAnyAction
{
    public byte Type;
    public fixed byte Data[7];
}