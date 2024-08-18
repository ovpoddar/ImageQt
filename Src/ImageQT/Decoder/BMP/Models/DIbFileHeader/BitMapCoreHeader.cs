using ImageQT.SourceGenerator.Abstract;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;

// 12
[DynamicValueGetter]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal partial struct BitMapCoreHeader
{
    public uint Size { get; set; }
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitDepth { get; set; }
}
