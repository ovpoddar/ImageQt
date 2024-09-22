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

    public BitFieldColorReader(Stream fileStream, BMPHeader RequiredProcessData)
      : base(fileStream, RequiredProcessData)
    {
        _redShift = CalculateMaskShift(ProcessData.RedMask);
        _blueShift = CalculateMaskShift(ProcessData.BlueMask);
        _greenShift = CalculateMaskShift(ProcessData.GreenMask);
        _redMaskSize = CalculateMaskSize(ProcessData.RedMask, _redShift);
        _blueMaskSize = CalculateMaskSize(ProcessData.BlueMask, _blueShift);
        _greenMaskSize = CalculateMaskSize(ProcessData.GreenMask, _greenShift);
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        // 16
        if (ProcessData.BitDepth == 16)
        {
            var value = BinaryPrimitives.ReadInt16LittleEndian(pixel);
            var r = MapTo8Bits((byte)((value & ProcessData.RedMask) >> _redShift), _redMaskSize);
            var g = MapTo8Bits((byte)((value & ProcessData.GreenMask) >> _greenShift), _greenMaskSize);
            var b = MapTo8Bits((byte)((value & ProcessData.BlueMask) >> _blueShift), _blueMaskSize);
            result[writingIndex++] = new Pixels(r, g, b);
        }
        // 32
        if (ProcessData.BitDepth == 24)
        {
            result[writingIndex++] = new Pixels(pixel[0], pixel[1], pixel[2]);
        }
    }
}