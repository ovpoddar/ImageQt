using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 56
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct BitMapV3InfoHeader
{
    public BMPHeaderType Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitDepth { get; set; }
    public HeaderCompression Compression { get; set; }
    public uint SizeImage { get; set; }
    public uint XPelsPerMeter { get; set; }
    public uint YPelsPerMeter { get; set; }
    public uint ClrUsed { get; set; }
    public uint ClrImportant { get; set; }
    public uint RedMask { get; set; }
    public uint GreenMask { get; set; }
    public uint BlueMask { get; set; }
    public uint AlphaMask { get; set; }

    public RequiredProcessData GetPropertyValue() =>
        (Height, Width, BitDepth, Compression);
}
