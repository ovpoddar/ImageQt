using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 36
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct CieXYZTriple
{
    public CieXYZ Red { get; set; }
    public CieXYZ Green { get; set; }
    public CieXYZ Blue { get; set; }

}
