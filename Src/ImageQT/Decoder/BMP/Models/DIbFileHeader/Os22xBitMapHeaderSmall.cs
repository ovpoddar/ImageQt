using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 16
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct Os22xBitMapHeaderSmall
{
    public uint Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitCount { get; set; }
}
