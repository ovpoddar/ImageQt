using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XtransConnFd
{
    public _XtransConnFd* Next;
    public int Fd;
    public int DoClose;
}