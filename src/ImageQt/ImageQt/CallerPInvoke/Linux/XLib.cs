using System;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Linux;

public static partial class XLib
{
    [LibraryImport("X11/x11.os", EntryPoint = "XOpenDisplayX")]
    public static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPWStr)] string? displayName);

    [LibraryImport("X11/x11.os", EntryPoint = "XDefaultScreenX")]
    public static partial Int32 XDefaultScreen(IntPtr display);

    [LibraryImport("X11/x11.os", EntryPoint = "XCreateSimpleWindowX")]
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

    [LibraryImport("X11/x11.os", EntryPoint = "XRootWindowX")]
    public static partial ulong XRootWindow(IntPtr display, int screen);

    [LibraryImport("X11/x11.os", EntryPoint = "XBlackPixelX")]
    public static partial ulong XBlackPixel(IntPtr display, int screen);

    [LibraryImport("X11/x11.os", EntryPoint = "XWhitePixelX")]
    public static partial ulong XWhitePixel(IntPtr display, int screen);

    [LibraryImport("X11/x11.os", EntryPoint = "XSelectInputX")]
    public static partial int XSelectInput(IntPtr display, ulong window, long event_mask);

    [LibraryImport("X11/x11.os", EntryPoint = "XMapWindowX")]
    public static partial int XMapWindow(IntPtr display, ulong window);

    [LibraryImport("X11/x11.os", EntryPoint = "XNextEventX")]
    public static partial int XNextEvent(IntPtr display, ref IntPtr XEvent);


    [LibraryImport("X11/x11.os", EntryPoint = "CreateWindow")]
    public static partial ulong CreateWindow(IntPtr display, ulong rootWindow, int screen, ulong border, ulong background);
}
