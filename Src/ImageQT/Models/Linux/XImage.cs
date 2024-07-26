#if DEBUG || Linux
using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux;

[StructLayout(LayoutKind.Sequential)]
public struct XImage
{
    public int width;
    public int height;
    public int xOffset;
    public int format;
    public IntPtr data;
    public int byte_order;
    public int bitmap_unit;
    public int bitmap_bit_order;
    public int bitmap_pad;
    public int depth;
    public int bytes_per_line;
    public int bits_per_pixel;
    public int red_mask;
    public int green_mask;
    public int blue_mask;
}
#endif
