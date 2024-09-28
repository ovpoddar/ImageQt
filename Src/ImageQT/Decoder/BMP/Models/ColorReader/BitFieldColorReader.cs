using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Models.ImagqQT;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class BitFieldColorReader : BaseColorReader
{
    private readonly byte _redShift;
    private readonly byte _blueShift;
    private readonly byte _greenShift;
    private readonly byte _redMaskSize;
    private readonly byte _blueMaskSize;
    private readonly byte _greenMaskSize;

    public BitFieldColorReader(BMPHeader RequiredProcessData)
      : base(RequiredProcessData)
    {
        _redShift = CalculateMaskShift(HeaderDetails.RedMask);
        _blueShift = CalculateMaskShift(HeaderDetails.BlueMask);
        _greenShift = CalculateMaskShift(HeaderDetails.GreenMask);
        _redMaskSize = CalculateMaskSize(HeaderDetails.RedMask, _redShift);
        _blueMaskSize = CalculateMaskSize(HeaderDetails.BlueMask, _blueShift);
        _greenMaskSize = CalculateMaskSize(HeaderDetails.GreenMask, _greenShift);
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        // 16
        if (HeaderDetails.BitDepth == 16)
        {
            var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
            var r = MapTo8Bits((byte)((value & HeaderDetails.RedMask) >> _redShift), _redMaskSize);
            var g = MapTo8Bits((byte)((value & HeaderDetails.GreenMask) >> _greenShift), _greenMaskSize);
            var b = MapTo8Bits((byte)((value & HeaderDetails.BlueMask) >> _blueShift), _blueMaskSize);
            result[writingIndex++] = new Pixels(r, g, b);
        }
        // 32
        if (HeaderDetails.BitDepth == 32)
        {
            // 32 bit has some issue
            var value = BinaryPrimitives.ReadInt32LittleEndian(pixel);
            var r = MapTo8Bits(((value & HeaderDetails.RedMask) >> _redShift), _redMaskSize);
            var g = MapTo8Bits(((value & HeaderDetails.GreenMask) >> _greenShift), _greenMaskSize);
            var b = MapTo8Bits(((value & HeaderDetails.BlueMask) >> _blueShift), _blueMaskSize);
            result[writingIndex++] = new Pixels(r, g, b);
        }
    }
}