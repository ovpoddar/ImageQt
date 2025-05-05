using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct PendingRequest
{
    public PendingRequest* Next;
    public ulong Sequence;
    public uint ReplyWaiter;
}