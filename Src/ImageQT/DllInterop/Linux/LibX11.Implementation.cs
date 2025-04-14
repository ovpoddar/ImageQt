using ImageQT.Models.Linux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
