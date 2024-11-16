﻿namespace ImageQT.Decoder.PNG.Models;
internal struct IHDRData
{
    public uint Width { get; set; }
    public uint Height { get; set; }
    public byte BitDepth { get; set; }
    public ColorType ColorType { get; set; }
    public byte CompressionMethod { get; set; }
    public byte FilterMethod { get; set; }
    public byte InterlaceMethod { get; set; }

    public IHDRData(PNGChunk headerChunk)
    {
        if (headerChunk.Length != 13)
            throw new ArgumentException("Invalid Header");

        Span<byte> response = stackalloc byte[(int)headerChunk.Length];
        headerChunk.GetData(response);

        var temp = response.Slice(0, 4);
        temp.Reverse();
        Width = BitConverter.ToUInt32(temp);

        temp = response.Slice(4, 4);
        temp.Reverse();
        Height = BitConverter.ToUInt32(temp);

        BitDepth = response[8];
        ColorType = (ColorType)response[9];
        CompressionMethod = response[10];
        FilterMethod = response[11];
        InterlaceMethod = response[12];

    }

    public readonly int GetScanLinesWidthWithPadding()
    {
        var length = Width * BitDepth * GetBytePerPixels();
        // 1 for the filter Type
        var count = (int)(length / 8) + 1;
        var extra = length % 8;

        if (extra == 0)
            return count;
        return ++count;
    }

    private readonly uint GetBytePerPixels() => ColorType switch
    {
        ColorType.GreyScale => 1,
        ColorType.RGB => 3,
        ColorType.Palette => 1,
        ColorType.GreyScaleAndAlpha => 2,
        ColorType.RGBA => 4,
        _ => throw new Exception(),
    };

    public readonly byte GetPixelSizeInByte() => ColorType switch
    {
        ColorType.GreyScale => (byte)Math.Round(1d * BitDepth / 8, MidpointRounding.ToPositiveInfinity),
        ColorType.Palette => 1,
        ColorType.GreyScaleAndAlpha => (byte)(2 * BitDepth / 8),
        ColorType.RGB => (byte)(3 * BitDepth / 8),
        ColorType.RGBA => (byte)(4 * BitDepth / 8),
        _ => throw new Exception()
    };
}
