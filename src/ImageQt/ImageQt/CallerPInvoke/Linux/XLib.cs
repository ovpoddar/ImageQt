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

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XWhitePixelX")]
    public static partial ulong XWhitePixel(IntPtr display, int screen);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XSelectInputX")]
    public static partial int XSelectInput(IntPtr display, ulong window, EventMask event_mask);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XMapWindowX")]
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XNextEventX")]
    public static partial int XNextEvent(IntPtr display, IntPtr XEvent);

    [LibraryImport("PlatformDll/X11.os", EntryPoint = "XCloseDisplayX")]
    public static partial int XCloseDisplay(IntPtr display);
}
