using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RleColorReader : BaseRLEColorReader
{
    private readonly ColorTable? _colorTable;

    public RleColorReader(Stream stream, BMPHeader header, ColorTable? colorTable) : base(stream, header) => 
        _colorTable = colorTable;

    protected override Pixels DefaultPixel => _colorTable.HasValue ? _colorTable.Value[0] : new Pixels();

    protected override void ProcessDefault(ArraySegment<Pixels> result, byte size, Span<byte> readByte)
    {
        if (HeaderDetails.BitDepth == 8)
        {
        var i = 0;
        foreach (var item in readByte)
        {
            if (i > result.Count)
                return;

            result[i++] = _colorTable!.Value[item];
        }
    }
        else if (HeaderDetails.BitDepth == 4)
        {
            var idx = 0;
            for (var i = 0; i < size; i++)
            {
                result[i] = _colorTable!.Value[(readByte[idx] >> ((i % 2) * 4)) & 0xF];
                if (i % 2 == 1) idx++;
            }
        }
    }

    protected override void ProcessFill(ArraySegment<Pixels> result, byte colorIndex)
    {
        if (HeaderDetails.BitDepth == 8)
        {
            result.AsSpan(0, result.Count)
            .Fill(_colorTable!.Value[colorIndex]);
    }
        else if (HeaderDetails.BitDepth == 4)
        {
            var rightPixel = _colorTable!.Value[colorIndex & 0xF];
            var leftPixel = _colorTable!.Value[(colorIndex >> 4) & 0xF];

            for (var i = 0; i < result.Count; i += 2)
            {
                result[i] = leftPixel;
                if (i + 1 < result.Count)
                    result[i + 1] = rightPixel;
            }

        }
    }
}
