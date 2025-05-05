using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XtransConnInfo
{
    public nint Transptr; //todo: think about _Xtransport 
    public int Index;
    public char* Priv;
    public int Flags;
    public int Fd;
    public char* Port;
    public int Family;
    public char* Addr;
    public int Addrlen;
    public char* Peeraddr;
    public int Peeraddrlen;
    public _XtransConnFd* RecvFds;
    public _XtransConnFd* SendFds;
}