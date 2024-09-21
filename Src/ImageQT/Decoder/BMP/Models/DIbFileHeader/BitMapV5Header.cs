﻿using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 124
[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct BitMapV5Header
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
    public uint AlphaMask { get; set; }
    public uint CSType { get; set; }
    public CieXYZTriple Endpoints { get; set; }
    public uint GammaRed { get; set; }
    public uint GammaGreen { get; set; }
    public uint GammaBlue { get; set; }
    public uint Intent { get; set; }
    public uint ProfileData { get; set; }
    public uint ProfileSize { get; set; }
    public uint Reserved { get; set; }

}
