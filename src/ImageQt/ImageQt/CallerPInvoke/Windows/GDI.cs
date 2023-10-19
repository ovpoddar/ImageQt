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
    public static extern IntPtr StretchDIBits(IntPtr hdc,
        int xDestination, 
        int yDestination, 
        int destinationWidth,
        int destinationHeight, 
        int xSource,
        int ySource,
        int sourceWidth, 
        int sourceHeight, 
        IntPtr memory, 
        ref BitmapInfo bitmap, 
        ColorUsage usage, 
        DropType dropType);

}
