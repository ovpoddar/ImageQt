// Ignore Spelling: Rgb

using ImageQT.Decoder.Models;
using System.Buffers.Binary;
using System.Diagnostics;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RgbColorReader : BaseColorReader
{
    private readonly ColorTable? _colorTable;
    private readonly byte _redShift;
    private readonly byte _blueShift;
    private readonly byte _greenShift;
    private readonly byte _redMaskSize;
    private readonly byte _blueMaskSize;
    private readonly byte _greenMaskSize;
    private readonly byte _step;
    private readonly byte _mask;
    public RgbColorReader(BMPHeader RequiredProcessData, ColorTable? colorTable)
        : base(RequiredProcessData)
    {
        _colorTable = colorTable;
        _redShift = CalculateMaskShift(HeaderDetails.RedMask);
        _blueShift = CalculateMaskShift(HeaderDetails.BlueMask);
        _greenShift = CalculateMaskShift(HeaderDetails.GreenMask);
        _redMaskSize = CalculateMaskSize(HeaderDetails.RedMask, _redShift);
        _blueMaskSize = CalculateMaskSize(HeaderDetails.BlueMask, _blueShift);
        _greenMaskSize = CalculateMaskSize(HeaderDetails.GreenMask, _greenShift);
        (_step, _mask) = GetDepthDetails();
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        switch (HeaderDetails.BitDepth)
        {
            case <= 8:
                {
                    Debug.Assert(pixel.Length == 1);
                    Debug.Assert(_colorTable.HasValue);
                    for (int j = _step; j >= 0; j -= HeaderDetails.BitDepth)
                    {
                        var currentBit = (byte)((pixel[0] & (byte)(_mask << j)) >> j);
                        if (writingIndex < HeaderDetails.Width)
                            result[writingIndex++] = _colorTable.Value[currentBit];
                    }

                    break;
                }

            case 16:
                {
                    Debug.Assert(pixel.Length == 2);
                    var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
                    var r = MapTo8Bits((byte)((value & HeaderDetails.RedMask) >> _redShift), _redMaskSize);
                    var g = MapTo8Bits((byte)((value & HeaderDetails.GreenMask) >> _greenShift), _greenMaskSize);
                    var b = MapTo8Bits((byte)((value & HeaderDetails.BlueMask) >> _blueShift), _blueMaskSize);
                    result[writingIndex++] = new Pixels(r, g, b);
                    break;
                }

            case 24:
                Debug.Assert(pixel.Length == 3);
                result[writingIndex++] = new Pixels(pixel[2], pixel[1], pixel[0]);
                break;
            case 32:
                /* TODO:ISSUE: according to Wikipidia 
                 * The 32-bit per pixel (32bpp) format supports 4,294,967,296 distinct colors and stores 1 pixel per 4-byte DWORD.
                 * Each DWORD can define the alpha, red, green and blue samples of the pixel.
                 * but found images in bgra format also can have MemoryMarshal.Cast if the format is bgra
                 * */
                Debug.Assert(pixel.Length == 4);
                result[writingIndex++] = new Pixels(pixel[2], pixel[1], pixel[0]);
                break;
        }
    }
}
