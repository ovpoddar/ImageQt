using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class Rle4BitColorReader : BaseRLEColorReader
{
    private readonly ColorTable? _colorTable;

    public Rle4BitColorReader(Stream stream, BMPHeader header, ColorTable? colorTable) : base(stream, header) =>
        _colorTable = colorTable;

    protected override Pixels DefaultPixel => _colorTable.HasValue ? _colorTable.Value[0] : new Pixels();

    protected override void ProcessDefault(ArraySegment<Pixels> result, byte size, Span<byte> readByte)
    {
        var idx = 0;
        for (var i = 0; i < size; i++)
        {
            result[i] = _colorTable!.Value[readByte[idx] >> (((i + 1) % 2) * 4) & 0xF];
            if (i % 2 == 1) idx++;
        }
    }

    protected override void ProcessFill(ArraySegment<Pixels> result, byte colorIndex)
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
