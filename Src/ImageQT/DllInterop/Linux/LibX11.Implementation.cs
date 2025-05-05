#if DEBUG || Linux
using ImageQT.Models.Linux;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ImageQT.DllInterop.Linux;
internal unsafe partial class LibX11
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Screen* ScreenOfDisplay(IntPtr display, int screen) =>
        ((XPrivateDisplay*)display.ToPointer())->screens + screen;

    public static ulong XBlackPixel(IntPtr display, int screen) =>
        ScreenOfDisplay(display, screen)->black_pixel;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong XWhitePixel(IntPtr display, int screen) =>
        ScreenOfDisplay(display, screen)->white_pixel;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int XDefaultScreen(IntPtr display) =>
        ((XPrivateDisplay*)display.ToPointer())->default_screen;

    public static ulong XRootWindow(IntPtr display, int screen) =>
        ScreenOfDisplay(display, screen)->root;

    public static IntPtr XDefaultVisual(IntPtr display, int screen) =>
        ScreenOfDisplay(display, screen)->root_visual;

    public static int XDefaultDepth(IntPtr display, int screen) =>
        ScreenOfDisplay(display, screen)->root_depth;

    private static void LockDisplay(IntPtr display)
    {
        if (((XDisplay*)display.ToPointer())->LockFns == null)
            return;
        ((XDisplay*)display.ToPointer())->LockFns->lock_display((XDisplay*)display, IntPtr.Zero, 0);
    }

    private static void UnlockDisplay(XDisplay* display)
    {
        if (display->LockFns == null)
            return;
        display->LockFns->unlock_display(display, IntPtr.Zero, 0);
    }

    private static void SyncHandle(XDisplay* display)
    {
        if (display->SyncHandler == null)
            return;
        display->SyncHandler(display);
    }

    private static uint XAllocID(IntPtr display)
    {
        return (uint)((XPrivateDisplay*)display.ToPointer())->resource_alloc(display);
    }

    private static IntPtr _XGetRequest(IntPtr display, int requestType, int length)
    {
        _xRequest* req;
        
        if (((XDisplay*)display.ToPointer())->BufferPointer + length > ((XDisplay*)display.ToPointer())->BufferMaximum)
            _XFlush(display);
        //Request still too large, so do not allow it to overflow. 
        if (((XDisplay*)display.ToPointer())->BufferPointer + length > ((XDisplay*)display.ToPointer())->BufferMaximum)
        {
            return IntPtr.Zero;
        }

        if (length % 4 == 0)
            Console.WriteLine("Xlib: request {requestType} length {length} not a multiple of 4.\n");

        ((XDisplay*)display.ToPointer())->LastRequest = ((XDisplay*)display.ToPointer())->BufferPointer;

        req = (_xRequest*)((XDisplay*)display.ToPointer())->BufferPointer;
        req->RequestType = (byte)requestType;
        req->Length = (byte)(length / 4);
        req->Data = 0;
        ((XDisplay*)display.ToPointer())->BufferPointer += length;
        ((XDisplay*)display.ToPointer())->Request++;
        return (nint)req;
    }

    static void _XFlush(IntPtr dpy)
    {
        var display = (XDisplay*)dpy.ToPointer();
        if (!RequireSocket(display))
            return;

        _XSend(display, IntPtr.Zero, 0);

        _XEventsQueued(dpy, 1);
    }

    public static bool RequireSocket(XDisplay* dpy)
    {
        if (dpy->BufferMaximum == dpy->Buffer)
        {
            XEventQueueOwner flag = 0;

            if (dpy->XCB->EventOwner != XEventQueueOwner.XLib)
                flag = XEventQueueOwner.XCB;

            if (xcb_take_socket(dpy->XCB->Connection, ReturnSocket, (IntPtr)dpy, flag, out var sent) != 1)
            {
                //_XIOError(display);
                return false;
            }
            dpy->XCB->LastFlushed = sent;
            dpy->Request = sent;
            dpy->BufferMaximum = dpy->XCB->RealBufmax;
        }
        return true;
    }

    private static void ReturnSocket(IntPtr display)
    {
        var dpy = (XDisplay*)display;
        InternalLockDisplay(dpy, /* don't skip user locks */ false);
        _XSend(dpy, IntPtr.Zero, 0);
        dpy->BufferMaximum = dpy->Buffer;
        UnlockDisplay(dpy);
    }

    private static void InternalLockDisplay(XDisplay* dpy, bool skipUserLocks)
    {
        if (dpy->Lock != null)
            dpy->Lock->InternalLockDisplay(dpy, skipUserLocks, IntPtr.Zero, 0);
    }

    public static void _XSend(XDisplay* display, IntPtr params1, int params2)
    {
        if ((display->Flags & 1L) != 0)
            return;

        if (display->BufferPointer == display->Buffer && params2 == 0)
            return;

        var displayRequest = display->Request;
        if (display->XCB->EventOwner != XEventQueueOwner.XLib || display->AsyncHandlers != IntPtr.Zero)
        {
            for (var i = display->XCB->LastFlushed; i < displayRequest; i++)
            {
                AppendPendingRequest(display, i);
            }
        }

        var requests = displayRequest - display->XCB->LastFlushed;
        display->XCB->LastFlushed = displayRequest;
        var vec = stackalloc IoVec[3];
        var pad = stackalloc byte[3];
        vec[0].IOVBase = display->Buffer;
        vec[0].IOVLength = (display->BufferPointer - display->Buffer);
        vec[1].IOVBase = params1;
        vec[1].IOVLength = params2;
        vec[2].IOVBase = (nint)pad;
        vec[2].IOVLength = -params2 & 3;

        _XExtension* extension;
        var dummyRequest = new _xRequest();

        for (extension = display->Flushes; extension != null; extension = extension->NextFlush)
        {
            int i;
            for (i = 0; i < 3; ++i)
                if (vec[i].IOVLength != 0)
                    extension->BeforeFlush(display, &extension->Codes, vec[i].IOVBase, vec[i].IOVLength);
        }

        if (xcb_writev(display->XCB->Connection, (IntPtr)vec, 3, requests) < 0)
        {
            Console.WriteLine("error..............");
            return;
        }
        display->BufferPointer = display->Buffer;
        display->LastRequest = (nint)(&dummyRequest);

        if (!CheckInternalConnections(display))
            return;

        _XSetSeqSyncFunction(display);
    }

    private static bool CheckInternalConnections(XDisplay* display)
    {
        if ((display->Flags & 16L) != 0 || display->ImFdInfo == null)
            return true;
        Console.WriteLine("Some free bsd code not here");

        fd_set r_mask = new();
        var highest_fd = -1;
        for (var ilist = display->ImFdInfo; ilist != null; ilist = ilist->Next)
        {
            Debug.Assert(ilist->Fd >= 0);
            FD_SET(ilist->Fd, (nint)(&r_mask));
            if (ilist->Fd > highest_fd)
                highest_fd = ilist->Fd;
        }
        Debug.Assert(highest_fd >= 0);

        var tv = new TimeVal
        {
            Sec = 0,
            Usec = 0
        };
        var result = select(highest_fd + 1, (nint)(&r_mask), IntPtr.Zero, IntPtr.Zero, (nint)(&tv));

        if (result == -1)
        {
            return true;
        }

        for (var ilist = display->ImFdInfo; result != 0; ilist = ilist->Next)
        {
            if (ilist == null)
                break;
            if (FD_ISSET(ilist->Fd, (nint)(&r_mask)) == 1)
            {
                _XProcessInternalConnection(display, ilist);
                --result;
            }
        }

        return true;
    }

    public static void _XSetSeqSyncFunction(XDisplay* display)
    {
        if (SyncHazard(display))
            _XSetPrivSyncFunction(display);
    }

    private static void _XSetPrivSyncFunction(XDisplay* display)
    {
        if (display->LockFns != null)
            return;

        if ((display->Flags & 8) == 0)
        {
            display->SavedSynChandler = display->SyncHandler;
            var syncHandler = Marshal.GetFunctionPointerForDelegate(_XPrivSyncFunction);
            display->SyncHandler = (delegate* unmanaged[Cdecl]<XDisplay*, int>)syncHandler;
            display->Flags |= 8;
        }
    }

    private static int _XPrivSyncFunction(XDisplay* display)
    {
        display->SyncHandler = display->SavedSynChandler;
        display->SavedSynChandler = null;
        display->Flags &= ~(ulong)8;
        if (display->SyncHandler != null)
            display->SyncHandler(display);
        _XIDHandler(display);
        _XSeqSyncFunction(display);
        return 0;
    }

    private static void _XSeqSyncFunction(XDisplay* display)
    {

        if ((display->Request - display->LastRequestRead) >= (ulong)(65535 - 2048 / Marshal.SizeOf<_xRequest>()))
        {
            var req = (_xRequest*)_XGetRequest((nint)display, 43, Marshal.SizeOf<_xRequest>());
            _XReply(display, (xReply*)&rep, 0, true);
            SyncWhileLocked(display);
        }
        else if (SyncHazard(display))
            _XSetPrivSyncFunction(display);
    }

    private static int _XReply(XDisplay* display, xReply* rep, int extra, bool discard)
    {
       
    }

    private static void SyncWhileLocked(XDisplay* display)
    {
        if (display->Lock != null)
            display->Lock->UserLockDisplay(display);
        UnlockDisplay(display);
        SyncHandle(display);
        InternalLockDisplay(display, /* don't skip user locks */ false);
        if (display->Lock != null)
            display->Lock->UserUnlockDisplay(display);
    }

    const ulong _invalidID = ~0UL;
    private static void _XIDHandler(XDisplay* display)
    {
        if (display->XCB->NextXid == _invalidID)
        {
            int i;
            if (display->Lock != null)
                display->Lock->UserLockDisplay(display);
            UnlockDisplay(display);
            for (i = 0; i < 1; i++)
                display->XCB->NextXid = xcb_generate_id(display->XCB->Connection);
            InternalLockDisplay(display, /* don't skip user locks */ false);
            if (display->Lock != null)
                display->Lock->UserUnlockDisplay(display);
        }
    }

    private static bool SyncHazard(XDisplay* display)
    {
        var span = display->Request - display->LastRequestRead;
        var hazard = Math.Min((display->BufferMaximum - display->Buffer) / Marshal.SizeOf<_xRequest>(), 65525);
        return span >= 65535 - (ulong)hazard - 10;
    }

    private static PendingRequest* AppendPendingRequest(XDisplay* display, ulong i)
    {
        var node = (PendingRequest*)Marshal.AllocHGlobal(Marshal.SizeOf<PendingRequest>());
        node->Next = null;
        node->Sequence = i;
        node->ReplyWaiter = 0;
        if (display->XCB->PendingRequestsTail != null)
        {
            if ((display->XCB->PendingRequestsTail->Sequence - node->Sequence) >= 0)
                throw new Exception("Unknown sequence number while appending request");
            if (display->XCB->PendingRequestsTail->Next != null)
                throw new Exception("Unknown request in queue  while appending request");
            display->XCB->PendingRequestsTail->Next = node;
        }
        else
            display->XCB->PendingRequests = node;
        display->XCB->PendingRequestsTail = node;
        return node;
    }

    private static void DequeuePendingRequest(XDisplay* dpy, PendingRequest* req)
    {
        if (req != dpy->XCB->PendingRequests)
            throw new Exception("Unknown request in queue while  dequeuing");

        dpy->XCB->PendingRequests = req->Next;
        if (dpy->XCB->PendingRequests != null)
        {
            if (req != dpy->XCB->PendingRequestsTail)
                throw new Exception("Unknown request in queue while dequeuing");
            dpy->XCB->PendingRequestsTail = null;
        }
        else if (req->Sequence - dpy->XCB->PendingRequests->Sequence >= 0)
            throw new Exception("Unknown sequence number while dequeuing request");

        Marshal.FreeHGlobal((nint)req);
    }

    public static ulong XCreateSimpleWindow(IntPtr display, ulong parentWindow, int x, int y, uint width, uint height, uint borderWidth, ulong border, ulong background)
    {
        ulong window = 0;

        LockDisplay(display);
        var request = (XCreateWindowRequest*)_XGetRequest(display, 1, Marshal.SizeOf<XCreateWindowRequest>() + 8);
        request->Parent = (uint)parentWindow;
        request->X = (short)x;
        request->Y = (short)y;
        request->Width = (ushort)width;
        request->Height = (ushort)height;
        request->BorderWidth = (ushort)borderWidth;
        request->Depth = 0;
        request->Class = 0;
        request->Visual = 0;
        window = request->Window = XAllocID(display);
        request->Mask = 10;

        {
            var valuePtr = (uint*)request++;
            *valuePtr++ = (uint)background;
            *valuePtr = (uint)border;
        }

        UnlockDisplay((XDisplay*)display);
        SyncHandle((XDisplay*)display);
        return window;
    }

}
#endif