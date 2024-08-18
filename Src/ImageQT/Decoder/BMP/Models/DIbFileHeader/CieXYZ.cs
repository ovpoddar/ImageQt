using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct CieXYZ
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
}
