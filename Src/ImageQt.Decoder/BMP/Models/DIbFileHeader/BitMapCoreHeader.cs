using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;

// 12
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct BitMapCoreHeader
{
    public BMPHeaderType Size { get; set; }
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitDepth { get; set; }

}
