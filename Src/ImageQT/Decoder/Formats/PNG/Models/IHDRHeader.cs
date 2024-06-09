using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.Formats.PNG.Models;

internal struct IHDRHeader
{
    public uint Width { get; set; }
    public uint Height { get; set; }
    public byte BitDepth { get; set; }
    public ColorType ColorType { get; set; }
    public byte CompressionMethod { get; set; }
    public byte FilterMethod { get; set; }
    public byte InterlaceMethod { get; set; }

    public IHDRHeader(Span<byte> span)
    {
        if (span.Length != 13)
            throw new ArgumentException("invalid data.");

        var _width = span.Slice(0, 4);
        _width.Reverse();
        var _height = span.Slice(4, 4);
        _height.Reverse();

        this.Width = BitConverter.ToUInt32(_width);
        this.Height = BitConverter.ToUInt32(_height);
        this.BitDepth = span[8];
        this.ColorType = (ColorType)span[9];
        this.CompressionMethod = span[10];
        this.FilterMethod = span[11];
        this.InterlaceMethod = span[12];
    }

    public int GetScanLinesWidth()
    {
        var scanLineLength = Width * BitDepth * GetBytePerPixels();
        var extraPixels = scanLineLength % 8;
        if (extraPixels == 0)
            return (int)(scanLineLength / 8) + 1;
        return (int)(scanLineLength += 8 - extraPixels) + 1;
    }

    private uint GetBytePerPixels() => this.ColorType switch
    {
        ColorType.GreyScale => 1,
        ColorType.RGB => 3,
        ColorType.Palette => 1,
        ColorType.GreyScaleAndAlpha => 2,
        ColorType.RGBA => 4,
        _ => throw new Exception(),
    };

    public byte[] AllowedBitDepths() => this.ColorType switch
    {
        ColorType.GreyScale => new byte[] { 1, 2, 4, 8, 16 },
        ColorType.RGB => new byte[] { 8, 16 },
        ColorType.Palette => new byte[] { 1, 2, 4, 8 },
        ColorType.GreyScaleAndAlpha => new byte[] { 8, 16 },
        ColorType.RGBA => new byte[] { 8, 16 },
        _ => throw new Exception(),
    };
}
