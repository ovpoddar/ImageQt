namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;
// 64
internal struct Os22xBitMapHeader
{
    public BMPHeaderType Size { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public ushort Planes { get; set; }
    public ushort BitCount { get; set; }
    public HeaderCompression Compression { get; set; }
    public uint SizeImage { get; set; }
    public int XPixelsPerMeter { get; set; }
    public int YPixelPerMeter { get; set; }
    public uint LrUsed { get; set; }
    public uint LrImportant { get; set; }
    public ushort Units { get; set; }
    public ushort Padding { get; set; }
    public Os22xBitMapHeaderDirection Recording { get; set; }
    public HalftoningAlgorithm Rendering { get; set; }
    public int Halftoning1 { get; set; }
    public int Halftoning2 { get; set; }
    public int ColorEncoding { get; set; }
    public int Identifier { get; set; }


    public RequiredProcessData GetPropertyValue() =>
        (Height, Width, BitCount, Compression);
}
