using ImageQT.Models.ImagqQT;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class AlphaBitFieldsReader : BaseColorReader
{
    private BMPHeader _header;

    public AlphaBitFieldsReader(BMPHeader header) : base(header)
    {
        _header = header;
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        if (base.HeaderDetails.BitDepth == 32)
        {

            var _redShift = CalculateMaskShift(HeaderDetails.RedMask);
            var _blueShift = CalculateMaskShift(HeaderDetails.BlueMask);
            var _greenShift = CalculateMaskShift(HeaderDetails.GreenMask);
            var _redMaskSize = CalculateMaskSize(HeaderDetails.RedMask, _redShift);
            var _blueMaskSize = CalculateMaskSize(HeaderDetails.BlueMask, _blueShift);
            var _greenMaskSize = CalculateMaskSize(HeaderDetails.GreenMask, _greenShift);
            var value = BinaryPrimitives.ReadInt32LittleEndian(pixel);
            var r = MapTo8Bits((byte)((value & HeaderDetails.RedMask) >> _redShift), _redMaskSize);
            var g = MapTo8Bits((byte)((value & HeaderDetails.GreenMask) >> _greenShift), _greenMaskSize);
            var b = MapTo8Bits((byte)((value & HeaderDetails.BlueMask) >> _blueShift), _blueMaskSize);
            result[writingIndex++] = new Pixels(r, g, b);
        }
    }
}
