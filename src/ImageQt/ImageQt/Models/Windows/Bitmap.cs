using System.Runtime.InteropServices;

namespace ImageQt.Models.Windows;
[StructLayout(LayoutKind.Sequential)]
public struct Bitmap
{
    public int bmType;
    public int bmWidth;
    public int bmHeight;
    public int bmWidthBytes;
    public ushort bmPlanes;
    public ushort bmBitsPixel;
    public IntPtr bmBits;
}
