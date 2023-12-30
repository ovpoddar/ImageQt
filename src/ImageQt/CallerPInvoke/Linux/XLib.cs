using ImageQt.Models.Linux;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Linux;

public static partial class XLib
{
    private const string XllAccessingDllPath = "PlatformDll/X11.os";

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XOpenDisplayX")]
    public static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPWStr)] string? displayName);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XDefaultScreenX")]
    public static partial Int32 XDefaultScreen(IntPtr display);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCreateSimpleWindowX")]
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

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XRootWindowX")]
    public static partial ulong XRootWindow(IntPtr display, int screen);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XBlackPixelX")]
    public static partial ulong XBlackPixel(IntPtr display, int screen);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XStoreNameX")]
    public static partial int XStoreName(IntPtr display, ulong window, [MarshalAs(UnmanagedType.LPStr)] string name);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XWhitePixelX")]
    public static partial ulong XWhitePixel(IntPtr display, int screen);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "DefaultDepthX")]
    public static partial uint DefaultDepth(IntPtr display, int screen);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "DefaultVisualX")]
    public static partial IntPtr DefaultVisual(IntPtr display, int screen);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XSelectInputX")]
    public static partial int XSelectInput(IntPtr display, ulong window, EventMask eventMask);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XMapWindowX")]
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XNextEventX")]
    public static partial int XNextEvent(IntPtr display, IntPtr xEvent);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCloseDisplayX")]
    public static partial int XCloseDisplay(IntPtr display);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCreateGCX")]
    public static partial IntPtr XCreateGC(IntPtr display, ulong window, ulong valueMask, IntPtr values);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCreateImageX")]
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

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCreatePixmapX")]
    public static partial ulong XCreatePixmap(
        IntPtr display,
        ulong window,
        uint width,
        uint height,
        uint depth);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XPutImageX")]
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

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XCopyAreaX")]
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

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XFreePixmapX")]
    public static partial void XFreePixmap(IntPtr display, ulong pixmap);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XFreeGCX")]
    public static partial void XFreeGC(IntPtr display, IntPtr gc);

    [LibraryImport(XllAccessingDllPath, EntryPoint = "XDestroyWindowX")]
    public static partial void XDestroyWindow(IntPtr display, ulong window);
}
