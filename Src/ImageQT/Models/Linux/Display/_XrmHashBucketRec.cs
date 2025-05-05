using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XrmHashBucketRec
{
    public nint Table; // todo: find way to do this _NTable
    public nint MbState;
    public XRMMethods* Methods;
    public nint LInfo; // todo: find way to do this LockInfoRec
}