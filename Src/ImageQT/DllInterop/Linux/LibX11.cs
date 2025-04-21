#if DEBUG || Linux
using ImageQT.Models.Linux;
using System.Runtime.InteropServices;

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
    public static partial int XNextEvent(IntPtr display, IntPtr xEvent);

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
    public static partial IntPtr _XGetRequest(IntPtr display, int reqType, int length);
}
#endif