using ImageQT.Models.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.DllInterop.Windows;
internal static partial class GDI32
{
    private const string _dllName = "gdi32.dll";
    [DllImport(_dllName)]
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

    [LibraryImport(_dllName)]
    public static partial IntPtr CreateSolidBrush(int color);
}
