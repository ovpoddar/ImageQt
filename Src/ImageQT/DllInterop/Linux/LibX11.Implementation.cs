#if DEBUG || Linux
using ImageQT.Models.Linux;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
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

    private static void UnlockDisplay(IntPtr display)
    {
        if (((XDisplay*)display.ToPointer())->LockFns == null)
            return;
        ((XDisplay*)display.ToPointer())->LockFns->unlock_display((XDisplay*)display, IntPtr.Zero, 0);
    }

    private static void SyncHandle(IntPtr display)
    {
        if (((XDisplay*)display.ToPointer())->SyncHandler == null)
            return;
        (((XDisplay*)display.ToPointer())->SyncHandler)((XDisplay*)display);
    }

    private static uint XAllocID(IntPtr display)
    {
        return (uint)((XPrivateDisplay*)display.ToPointer())->resource_alloc(display);
    }
    /*
    private static IntPtr _XGetRequest(IntPtr display, int reqType, int length)
    {
        xReq* req;

        if (dpy->bufptr + len > dpy->bufmax)
            _XFlush(dpy);
         //Request still too large, so do not allow it to overflow. 
        if (dpy->bufptr + len > dpy->bufmax)
        {
            fprintf(stderr,
                "Xlib: request %d length %zd would exceed buffer size.\n",
                type, len);
    //Changes failure condition from overflow to NULL dereference.
            return NULL;
        }

        if (len % 4)
            fprintf(stderr,
                "Xlib: request %d length %zd not a multiple of 4.\n",
                type, len);

        dpy->last_req = dpy->bufptr;

        req = (xReq*)dpy->bufptr;
        *req = (xReq) {
        .reqType = type,
        .data = 0,
        .length = len / 4
        }
        ;
        dpy->bufptr += len;
        X_DPY_REQUEST_INCREMENT(dpy);
        return req;
    }
*/
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

        //   {
        //register CARD32 *valuePtr = (CARD32*) NEXTPTR(request, xCreateWindowReq);
        //*valuePtr++ = background;
        //*valuePtr = border;
        //   }

        UnlockDisplay(display);
        SyncHandle(display);
        return window;
    }

}
#endif