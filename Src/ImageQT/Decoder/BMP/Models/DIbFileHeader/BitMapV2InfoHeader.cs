﻿using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 52
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct BitMapV2InfoHeader
{
    public BMPHeaderType Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitDepth { get; set; }
    public HeaderCompression Compression { get; set; }
    public uint SizeImage { get; set; }
    public int XPelsPerMeter { get; set; }
    public int YPelsPerMeter { get; set; }
    public uint ColorUsed { get; set; }
    public uint ClrImportant { get; set; }
    public uint RedMask { get; set; }
    public uint GreenMask { get; set; }
    public uint BlueMask { get; set; }

}