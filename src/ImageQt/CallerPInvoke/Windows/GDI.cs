using ImageQt.Models.Windows;
using System;
using System.Runtime.InteropServices;

namespace ImageQt.CallerPInvoke.Windows;
internal static partial class GDI
{
    [DllImport(dllName: "gdi32.dll", EntryPoint = "StretchDIBits")]
    public static extern IntPtr StretchDIBits([In] IntPtr hdc,
        [In] int xDestination,
        [In] int yDestination,
        [In] int destinationWidth,
        [In] int destinationHeight,
        [In] int xSource,
        [In] int ySource,
        [In] int sourceWidth,
        [In] int sourceHeight,
        [In] IntPtr memory,
        [In] ref BitmapInfo bitmap,
        [In] ColorUsage usage,
        [In] DropType dropType);

}
