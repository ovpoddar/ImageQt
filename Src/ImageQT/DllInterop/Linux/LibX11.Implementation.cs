#if DEBUG || Linux
using ImageQT.Models.Linux;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
}
#endif