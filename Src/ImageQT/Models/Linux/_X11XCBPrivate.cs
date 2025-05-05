using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _X11XCBPrivate
{
    public IntPtr Connection;
    public PendingRequest* PendingRequests;
    public PendingRequest* PendingRequestsTail;
    public XCBGenericEvent* next_event;
    public IntPtr NextResponse;
    public IntPtr RealBufmax;
    public IntPtr ReplyData;
    public int ReplyLength;
    public int ReplyConsumed;
    public ulong LastFlushed;
    public XEventQueueOwner EventOwner;
    public ulong NextXid;

    /* handle simultaneous threads waiting for responses */
    public IntPtr EventNotify;
    public int EventWaiter;
    public IntPtr ReplyNotify;
}
