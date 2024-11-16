using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct BmpFileHeader
{
    public ushort FileType { get; set; }
    public uint FileSize { get; set; }
    public ushort Reserved1 { get; set; }
    public ushort Reserved2 { get; set; }
    public uint OffsetData { get; set; }
}
