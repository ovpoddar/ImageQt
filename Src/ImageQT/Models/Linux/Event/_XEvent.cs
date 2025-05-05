using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Event;

[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 192)]
public unsafe struct _XEvent
{
    public fixed byte Context[192];
}