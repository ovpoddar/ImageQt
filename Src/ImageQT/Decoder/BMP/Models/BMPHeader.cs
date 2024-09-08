using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Helpers;
using ImageQT.Exceptions;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models;
internal struct BMPHeader
{
    public int Height { get; set; }
    public int Width { get; set; }
    public int BitDepth { get; set; }
    public HeaderCompression Compression { get; set; }

    // TODO: find better way to the switch without dynamic
    public BMPHeader(Stream stream)
    {
        var sizeofBMPHeaderType = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(BMPHeaderType))); ;
        Span<byte> bmpHeaderType = stackalloc byte[sizeofBMPHeaderType];
        stream.Read(bmpHeaderType);
        dynamic header = bmpHeaderType.ToStruct<BMPHeaderType>() switch
        {
            BMPHeaderType.BitMapCore => stream.FromStream<BitMapCoreHeader>(-sizeofBMPHeaderType),
            BMPHeaderType.OS22XBitMapSmall => stream.FromStream<Os22xBitMapHeaderSmall>(-sizeofBMPHeaderType),
            BMPHeaderType.BitMapINFO => stream.FromStream<BitMapInfoHeader>(-sizeofBMPHeaderType),
            BMPHeaderType.BitMapV2INFO => stream.FromStream<BitMapV2InfoHeader>(-sizeofBMPHeaderType),
            BMPHeaderType.BitMapV3INFO => stream.FromStream<BitMapV3InfoHeader>(-sizeofBMPHeaderType),
            BMPHeaderType.OS22XBitMap => stream.FromStream<Os22xBitMapHeader>(-sizeofBMPHeaderType),
            BMPHeaderType.BitMapV4 => stream.FromStream<BitMapV4Header>(-sizeofBMPHeaderType),
            BMPHeaderType.BitMapV5 => stream.FromStream<BitMapV5Header>(-sizeofBMPHeaderType),
            _ => throw new BadImageException(),
        };

        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        try
        {
            this.Compression = header.Compression;
        }
        catch
        {
            this.Compression = HeaderCompression.Rgb;
        }
    }

    public int GetNormalizeHeight() =>
        this.Height < 0 ? this.Height * -1 : this.Height;

    public readonly int GetMinimumPixelsSizeInByte() => this.BitDepth switch
    {
        1 => 1,
        2 => 1,
        4 => 1,
        8 => 1,
        16 => 2,
        24 => 3,
        32 => 4,
        _ => throw new BadImageException(),
    };
}
