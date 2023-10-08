using ImageQt.Models;
using ImageQt.Models.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.CallerPInvoke.Windows;
internal static partial class GDI
{

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DeleteDC(nint mncdc);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool DeleteObject(nint hwnd);

    [LibraryImport("gdi32.dll")]
    public static partial nint CreateCompatibleDC(IntPtr hdc);

    [LibraryImport("gdi32.dll")]
    public static partial nint SelectObject(nint hdc, nint hgdiobj);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool BitBlt(nint hdc, int x, int y, int cx, int cy, nint hdcSrc, int x1, int y1, uint rop);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern int GetObject(IntPtr hgdiobj, int cbBuffer, out Bitmap lpvObject);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateDIBitmap(IntPtr hdc, IntPtr pbmih, uint flInit, IntPtr pjBits, BitmapInfoHeader pbmi, uint iUsage);

}
