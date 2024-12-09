using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Exceptions;
using ImageQT.Decoder.Helpers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ImageQT.Decoder.BMP.Models;
internal struct BMPHeader
{
    public int ColorUsed { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int BitDepth { get; set; }
    public HeaderCompression Compression { get; set; }
    public BMPHeaderType Type { get; set; }
    public uint RedMask { get; set; }
    public uint BlueMask { get; set; }
    public uint GreenMask { get; set; }

    public BMPHeader(Stream stream)
    {
        var sizeofBMPHeaderType = Marshal.SizeOf<uint>();
        Span<byte> bmpHeaderType = stackalloc byte[sizeofBMPHeaderType];
        stream.Read(bmpHeaderType);
        this.Type = bmpHeaderType.ToStruct<BMPHeaderType>();
        switch (Type)
        {
            case BMPHeaderType.BitMapCore:
                var bitMapCore = stream.FromStream<BitMapCoreHeader>(-sizeofBMPHeaderType);
                InitializedFields(bitMapCore);
                break;
            case BMPHeaderType.OS22XBitMapSmall:
                var oS22XBitMapSmall = stream.FromStream<Os22xBitMapHeaderSmall>(-sizeofBMPHeaderType);
                InitializedFields(oS22XBitMapSmall);
                break;
            case BMPHeaderType.BitMapINFO:
                var bitMapINFO = stream.FromStream<BitMapInfoHeader>(-sizeofBMPHeaderType);
                InitializedFields(bitMapINFO);
                break;
            case BMPHeaderType.BitMapV2INFO:
                var bitMapV2INFO = stream.FromStream<BitMapV2InfoHeader>(-sizeofBMPHeaderType);
                InitializedFields(bitMapV2INFO);
                break;
            case BMPHeaderType.BitMapV3INFO:
                var bitMapV3INFO = stream.FromStream<BitMapV3InfoHeader>(-sizeofBMPHeaderType);
                InitializedFields(bitMapV3INFO);
                break;
            case BMPHeaderType.OS22XBitMap:
                var oS22XBitMap = stream.FromStream<Os22xBitMapHeader>(-sizeofBMPHeaderType);
                InitializedFields(oS22XBitMap);
                break;
            case BMPHeaderType.BitMapV4:
                var bitMapV4 = stream.FromStream<BitMapV4Header>(-sizeofBMPHeaderType);
                InitializedFields(bitMapV4);
                break;
            case BMPHeaderType.BitMapV5:
                var bitMapV5 = stream.FromStream<BitMapV5Header>(-sizeofBMPHeaderType);
                InitializedFields(bitMapV5);
                break;
            default:
                throw new BadImageException();
        }
    }

    private void InitializedFields(BitMapCoreHeader header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = HeaderCompression.Rgb;
        this.RedMask = this.Compression == HeaderCompression.BitFields
                ? 0b1111100000000000u
                : 0b0111110000000000u;
        this.GreenMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u;
        this.BlueMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u;
        this.ColorUsed = 0;
    }
    private void InitializedFields(Os22xBitMapHeaderSmall header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = HeaderCompression.Rgb;
        this.RedMask = this.Compression == HeaderCompression.BitFields
              ? 0b1111100000000000u
              : 0b0111110000000000u;
        this.GreenMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u;
        this.BlueMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u;
        this.ColorUsed = 0;
    }

    private void InitializedFields(BitMapInfoHeader header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = this.Compression == HeaderCompression.BitFields
                ? 0b1111100000000000u
                : 0b0111110000000000u;
        this.GreenMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u;
        this.BlueMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u;
        this.ColorUsed = (int)header.ColorUsed;
    }

    private void InitializedFields(BitMapV2InfoHeader header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = header.RedMask;
        this.GreenMask = header.GreenMask;
        this.BlueMask = header.BlueMask;
        this.ColorUsed = (int)header.ColorUsed;
    }

    private void InitializedFields(BitMapV3InfoHeader header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = this.Compression == HeaderCompression.BitFields
                ? 0b1111100000000000u
                : 0b0111110000000000u;
        this.GreenMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u;
        this.BlueMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u;
        this.ColorUsed = (int)header.ColorUsed;
    }

    private void InitializedFields(Os22xBitMapHeader header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = this.Compression == HeaderCompression.BitFields
                ? 0b1111100000000000u
                : 0b0111110000000000u;
        this.GreenMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u;
        this.BlueMask = this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u;
        this.ColorUsed = 0;
    }

    private void InitializedFields(BitMapV4Header header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = header.RedMask;
        this.GreenMask = header.GreenMask;
        this.BlueMask = header.BlueMask;
        this.ColorUsed = (int)header.ColorUsed;
    }

    private void InitializedFields(BitMapV5Header header)
    {
        this.BitDepth = header.BitDepth;
        this.Width = header.Width;
        this.Height = header.Height;
        this.Compression = header.Compression;
        this.RedMask = header.RedMask;
        this.GreenMask = header.GreenMask;
        this.BlueMask = header.BlueMask;
        this.ColorUsed = (int)header.ColorUsed;
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

    public void ReadFromExtraBits(Span<byte> masks)
    {
        Debug.Assert(masks.Length >= 12);
        RedMask = BitConverter.ToUInt32(masks.Slice(0, 4));
        GreenMask = BitConverter.ToUInt32(masks.Slice(4, 4));
        BlueMask = BitConverter.ToUInt32(masks.Slice(8, 4));
    }

    public readonly int? CalculateTheSizeOfExtraBitMask(int availableByte)
    {
        if (this.Type != BMPHeaderType.BitMapINFO
            || this.Compression is not HeaderCompression.BitFields and not HeaderCompression.AlphaBitFields
            || this.BitDepth is not 16 and not 32)
            return null;

        var requiredSizeForBitMask = BitDepth == 16 ? 12 : 16;
        return availableByte >= requiredSizeForBitMask
            ? requiredSizeForBitMask
            : availableByte;
    }

    public readonly int? CalculateDetailsOfPalate(int availableByte)
    {
        if (BitDepth <= 8 || ColorUsed > 0)
        {
            var pixelsCount = ColorUsed > 0
               ? this.ColorUsed
               : (0xff >> (8 - this.BitDepth)) + 1;
            if (pixelsCount == 0)
                return null;

            var pixelSize = CalculatePixelSize();
            var requiredSizeForColorTable = pixelsCount * pixelSize;
            return availableByte >= requiredSizeForColorTable
                ? requiredSizeForColorTable
                : availableByte;
        }
        return null;
    }

    public readonly int CalculatePixelSize() =>
        this.Type is BMPHeaderType.BitMapCore ? 3 : 4;

    public readonly bool IsDependsOnExternalDecoding() =>
        this.Compression is HeaderCompression.Png or HeaderCompression.Jpeg;
}
