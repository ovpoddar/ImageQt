using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Decoder.Helpers;
using ImageQT.Exceptions;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
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
        var sizeofBMPHeaderType = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(BMPHeaderType))); ;
        Span<byte> bmpHeaderType = stackalloc byte[sizeofBMPHeaderType];
        stream.Read(bmpHeaderType);
        this.Type = bmpHeaderType.ToStruct<BMPHeaderType>();
        dynamic header = Type switch
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
        this.Width = (int)header.Width;
        this.Height = (int)header.Height;
        this.Compression = Type switch
        {
            BMPHeaderType.BitMapCore => HeaderCompression.Rgb,
            BMPHeaderType.OS22XBitMapSmall => HeaderCompression.Rgb,
            _ => header.Compression,
        };

        this.RedMask = Type switch
        {
            BMPHeaderType.BitMapV2INFO => (uint)header.RedMask,
            BMPHeaderType.BitMapV3INFO => (uint)header.RedMask,
            BMPHeaderType.BitMapV4 => (uint)header.RedMask,
            BMPHeaderType.BitMapV5 => (uint)header.RedMask,
            _ => this.Compression == HeaderCompression.BitFields
                ? 0b1111100000000000u
                : 0b0111110000000000u
        };
        this.GreenMask = Type switch
        {
            BMPHeaderType.BitMapV2INFO => (uint)header.GreenMask,
            BMPHeaderType.BitMapV3INFO => (uint)header.GreenMask,
            BMPHeaderType.BitMapV4 => (uint)header.GreenMask,
            BMPHeaderType.BitMapV5 => (uint)header.GreenMask,
            _ => this.Compression == HeaderCompression.BitFields
                ? 0b0000011111100000u
                : 0b0000001111100000u
        };
        this.BlueMask = Type switch
        {
            BMPHeaderType.BitMapV2INFO => (uint)header.BlueMask,
            BMPHeaderType.BitMapV3INFO => (uint)header.BlueMask,
            BMPHeaderType.BitMapV4 => (uint)header.BlueMask,
            BMPHeaderType.BitMapV5 => (uint)header.BlueMask,
            _ => this.Compression == HeaderCompression.BitFields
                ? 0b0000000000011111u
                : 0b0000000000011111u
        };

        this.ColorUsed = Type switch
        {
            BMPHeaderType.BitMapINFO => (int)header.ColorUsed,
            BMPHeaderType.BitMapV2INFO => (int)header.ColorUsed,
            BMPHeaderType.BitMapV3INFO => (int)header.ColorUsed,
            BMPHeaderType.BitMapV4 => (int)header.ColorUsed,
            BMPHeaderType.BitMapV5 => (int)header.ColorUsed,
            _ => 0
        };
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
            : null;
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

    /*
     * For bitmaps with normal color tables (where the compression scheme is not BITFIELDS encoding),
     * the length of the color table is a function of the number of colors the image can have. The 
     * number of possible colors is 2bit depth, where bit depth is the number of color planes, multiplied
     * by the number of bits per plane. If bit depth is 24, however, the length of the color table is 0,
     * since each pixel will already contain the proper RGB value. Each entry in a normal color table is one RGB structure.
     * The first byte is the blue value; the second, green; and the third, red. If this is a "new" format bitmap 
     * (indicated by the image's BITMAPHEADER structure having a size greater than 12 bytes),
     * then an additional byte of padding between each RGB structure must be skipped over before reading the 
     * next structure in the array.
     */
    public readonly int CalculatePixelSize()
    {
        if (this.Compression is HeaderCompression.Rle4 or HeaderCompression.Rle8)
            return 2;
        if (this.Type is BMPHeaderType.BitMapCore)
            return 3;
        return 4;
    }
}
