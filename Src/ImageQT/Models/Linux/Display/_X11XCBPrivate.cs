using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _X11XCBPrivate
{
    public nint Connection;
    public PendingRequest* PendingRequests;
    public PendingRequest* PendingRequestsTail;
    public XCBGenericEvent* nextEvent;
    public nint NextResponse;
    public nint RealBufmax;
    public nint ReplyData;
    public int ReplyLength;
    public int ReplyConsumed;
    public ulong LastFlushed;
    public XEventQueueOwner EventOwner;
    public ulong NextXid;

    /* handle simultaneous threads waiting for responses */
    public nint EventNotify;
    public int EventWaiter;
    public nint ReplyNotify;
}