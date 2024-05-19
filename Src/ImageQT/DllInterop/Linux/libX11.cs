using ImageQT.Models.Linux;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.DllInterop.Linux;
internal partial class LibX11
{
    private const string _dllName = "libX11.so";

    [LibraryImport(_dllName)]
    public static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPWStr)] string? displayName);

    [LibraryImport(_dllName)]
    public static partial int XDefaultScreen(IntPtr display);

    [LibraryImport(_dllName)]
    public static partial ulong XCreateSimpleWindow(
       IntPtr display,
       ulong parentWindow,
       int x,
       int y,
       uint width,
       uint height,
       uint borderWidth,
       ulong border,
       ulong background
   );

    [LibraryImport(_dllName)]
    public static partial ulong XRootWindow(IntPtr display, int screen);

    [LibraryImport(_dllName)]
    public static partial ulong XBlackPixel(IntPtr display, int screen);

    [LibraryImport(_dllName)]
    public static partial ulong XWhitePixel(IntPtr display, int screen);

    [LibraryImport(_dllName)]
    public static partial IntPtr XDefaultVisual(IntPtr display, int screen);

    [LibraryImport(_dllName)]
    public static partial uint XDefaultDepth(IntPtr display, int screen);

    [LibraryImport(_dllName)]
    public static partial int XSelectInput(IntPtr display, ulong window, EventMask eventMask);

    [LibraryImport(_dllName)]
    public static partial ulong XInternAtom(IntPtr display, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.Bool)] bool state);
    
    [LibraryImport(_dllName)]
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport(_dllName)]
    public static partial int XNextEvent(IntPtr display, IntPtr xEvent);

    [LibraryImport(_dllName)]
    public static partial int XSetWMProtocols(IntPtr display, ulong window, IntPtr atom, int count);

    [LibraryImport(_dllName)]
    public static partial int XDestroyWindow(IntPtr display, ulong window);

    [LibraryImport(_dllName)]
    public static partial IntPtr XCreateGC(IntPtr display, ulong window, ulong valueMask, IntPtr values);

    [LibraryImport(_dllName)]
    public static partial int XCloseDisplay(IntPtr display);

    [LibraryImport(_dllName)]
    public static partial IntPtr XCreateImage(
       IntPtr display,
       IntPtr visual,
       uint depth,
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
       uint depth);

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
    public static partial void XFreePixmap(IntPtr display, ulong pixmap);

    [LibraryImport(_dllName)]
    public static partial void XFreeGC(IntPtr display, IntPtr gc);

    [LibraryImport(_dllName)]
    public static partial void XDestroyImage(IntPtr xImage);
}
