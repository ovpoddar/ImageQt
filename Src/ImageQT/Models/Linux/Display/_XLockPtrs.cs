using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XLockPtrs
{
    public delegate* unmanaged[Cdecl]<XDisplay*, nint, int, void> LockDisplay;

    public delegate* unmanaged[Cdecl]<XDisplay*, nint, int, void> UnlockDisplay;
}