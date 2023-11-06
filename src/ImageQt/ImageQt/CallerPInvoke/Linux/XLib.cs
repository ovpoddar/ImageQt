using ImageQt.Models.Linux;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Linux;

public static partial class XLib
{
    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XOpenDisplayX")]
    public static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPWStr)] string? displayName);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XDefaultScreenX")]
    public static partial Int32 XDefaultScreen(IntPtr display);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCreateSimpleWindowX")]
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

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XRootWindowX")]
    public static partial ulong XRootWindow(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XBlackPixelX")]
    public static partial ulong XBlackPixel(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XStoreNameX")]
    public static partial int XStoreName(IntPtr display, ulong window, [MarshalAs(UnmanagedType.LPStr)] string name);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XWhitePixelX")]
    public static partial ulong XWhitePixel(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "DefaultDepthX")]
    public static partial uint DefaultDepth(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "DefaultVisualX")]
    public static partial IntPtr DefaultVisual(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XSelectInputX")]
    public static partial int XSelectInput(IntPtr display, ulong window, EventMask eventMask);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XMapWindowX")]
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XNextEventX")]
    public static partial int XNextEvent(IntPtr display, IntPtr xEvent);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCloseDisplayX")]
    public static partial int XCloseDisplay(IntPtr display);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCreateGCX")]
    public static partial IntPtr XCreateGC(IntPtr display, ulong window, ulong valueMask, IntPtr values);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCreateImageX")]
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

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCreatePixmapX")]
    public static partial ulong XCreatePixmap(
        IntPtr display,
        ulong window,
        uint width,
        uint height,
        uint depth);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XPutImageX")]
    public static partial void XPutImage(IntPtr display,
        ulong drawable,
        IntPtr gc,
        IntPtr imageData,
        int srcX,
        int srcY,
        int destX,
        int destY,
        uint width,
        uint height);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCopyAreaX")]
    public static partial void XCopyArea(IntPtr display,
        ulong src,
        ulong dest,
        IntPtr graphicsContext,
        int srcX,
        int srcY,
        uint width,
        uint height,
        int destX,
        int destY);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XFreePixmapX")]
    public static partial void XFreePixmap(IntPtr display, ulong pixmap);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XFreeGCX")]
    public static partial void XFreeGC(IntPtr display, IntPtr gc);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XDestroyWindowX")]
    public static partial void XDestroyWindow(IntPtr display, ulong window);
}
