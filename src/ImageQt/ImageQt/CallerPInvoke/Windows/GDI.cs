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
    [DllImport("gdi32.dll")]
    public static extern IntPtr StretchDIBits(IntPtr hdc, int xDest, int yDest, int destWidth, int destHeight, int xSrc, int ySrc, int srcWidth, int srcHeight, IntPtr memory, ref BitmapInfo bitmap, ColorUsage usage, DropType dwRop);

}
