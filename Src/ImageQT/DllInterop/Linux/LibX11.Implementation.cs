#if DEBUG || Linux
using ImageQT.Models.Linux;
using System.Runtime.CompilerServices;

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
}
#endif