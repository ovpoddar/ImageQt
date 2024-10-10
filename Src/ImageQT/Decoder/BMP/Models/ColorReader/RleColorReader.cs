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
        var i = 0;
        foreach (var item in readByte)
        {
            if (i > result.Count)
                return;

            result[i++] = _colorTable!.Value[item];
        }
    }

    protected override void ProcessFill(ArraySegment<Pixels> result, byte size, byte colorIndex)
    {
        result.AsSpan(0, size)
            .Fill(_colorTable!.Value[colorIndex]);
    }
}
