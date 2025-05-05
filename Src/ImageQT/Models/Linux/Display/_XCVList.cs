using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XCVList
{
    public nint Cv;
    public nint Buf;
    public _XCVList* Next;
}