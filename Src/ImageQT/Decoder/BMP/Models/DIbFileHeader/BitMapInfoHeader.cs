using ImageQT.SourceGenerator.Abstract;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 40
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct BitMapInfoHeader
{
    public uint Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitCount { get; set; }
    public uint Compression { get; set; }
    public uint SizeImage { get; set; }
    public int XPixelsPerMeter { get; set; }
    public int YPixelPerMeter { get; set; }
    public uint ColorUsed { get; set; }
    public uint ColorImportant { get; set; }

}
