#if DEBUG || Linux
using ImageQT.Models.Linux;
using System.Runtime.InteropServices;
using static ImageQT.Models.Linux.X11Delegates;
using ImageQT.Models.Linux.Event;
using ImageQT.Models.Linux.Display;

namespace ImageQT.DllInterop.Linux;
internal partial class LibX11
{
    private const string _dllName = "libX11.so";

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/OpenDis.c 500 line changes
    public static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPWStr)] string? displayName);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/SelInput.c
    public static partial int XSelectInput(IntPtr display, ulong window, EventMask eventMask);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/IntAtom.c
    public static partial ulong XInternAtom(IntPtr display, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.Bool)] bool state);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/MapWindow.c
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/NextEvent.c
    public static unsafe partial int XNextEvent(IntPtr display, _XEvent* xEvent);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/SetWMProto.c
    public static partial int XSetWMProtocols(IntPtr display, ulong window, IntPtr atom, int count);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/CrGC.c
    public static partial IntPtr XCreateGC(IntPtr display, ulong window, ulong valueMask, IntPtr values);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/ClDisplay.c
    public static partial int XCloseDisplay(IntPtr display);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/ImUtil.c
    public static partial IntPtr XCreateImage(
       IntPtr display,
       IntPtr visual,
       int depth,
       ImageFormat format,
       int offset,
       IntPtr data,
       uint width,
       uint height,
       int bitmapPad,
       int bytesPerLine);

    [LibraryImport(_dllName)]
    public static partial ulong XCreatePixmap(
       IntPtr display,
       ulong window,
       uint width,
       uint height,
       int depth);

    [LibraryImport(_dllName)]
    public static partial void XPutImage(IntPtr display,
       ulong drawable,
       GraphicsContext gc,
       IntPtr imageData,
       int srcX,
       int srcY,
       int destX,
       int destY,
       uint width,
       uint height);

    [LibraryImport(_dllName)]
    public static partial void XCopyArea(IntPtr display,
      ulong src,
      ulong dest,
      GraphicsContext graphicsContext,
      int srcX,
      int srcY,
      uint width,
      uint height,
      int destX,
      int destY);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/FreePix.c
    public static partial void XFreePixmap(IntPtr display, ulong pixmap);

    [LibraryImport(_dllName)]
    // /home/ayan/projects/libx11/src/FreeGC.c
    public static partial void XFreeGC(IntPtr display, IntPtr gc);

    [LibraryImport(_dllName)]
    // this is a wrapper for stdlib.h free call
    public static partial void XFree(IntPtr display);

    [LibraryImport(_dllName)]
    public static partial int _XEventsQueued(IntPtr display, int mode);

    //[DllImport("libc", ExactSpelling = true)]
    //public static extern int select(int __nfds, fd_set* __readfds, fd_set* __writefds, fd_set* __exceptfds, [NativeTypeName("struct timeval *")] timeval* __timeout);

    [LibraryImport("libc")]
    public static partial void FD_ZERO(IntPtr set);

    [LibraryImport("libc")]
    public static partial void FD_SET(int fd, IntPtr set);

    [LibraryImport("libc")]
    public static partial int FD_ISSET(int fd, IntPtr set);

    [LibraryImport("libc")]
    public static partial int select(int nfds, IntPtr readfds, IntPtr writefds, IntPtr exceptfds, IntPtr timeout);

    // requir libxcb1-dev
    public const string _dllName1 = "libxcb.so.1";

    [LibraryImport(_dllName1)]
    public static partial int xcb_take_socket(IntPtr connection, ReturnSocket returnSocket, IntPtr closure, XEventQueueOwner flags, out ulong sent);

    [LibraryImport(_dllName1)]
    public static partial int xcb_writev(IntPtr connection, IntPtr vector, int count, ulong request);

    [LibraryImport(_dllName1)]
    public static partial ulong xcb_generate_id(IntPtr connection);
}
#endif