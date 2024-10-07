using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RleColorReader : BaseColorReader
{
    private readonly Pixels _defaultPixel;
    private readonly ColorTable? _colorTable;

    public override bool IsRLE => true;
    public RleColorReader(BMPHeader header, ColorTable? colorTable) : base(header)
    {
        _colorTable = colorTable;
        _defaultPixel = colorTable.HasValue ? colorTable.Value[0] : new Pixels();
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex, bool isUndefinedPixels)
    {
        if (isUndefinedPixels)
        {
            var availableSpace =  result.Count - writingIndex;
            result.AsSpan(writingIndex, availableSpace < pixel.Length ? availableSpace : pixel.Length).Fill(_defaultPixel);
            writingIndex += pixel.Length;
            return;
        }


        foreach (var item in pixel)
        {
            if (HeaderDetails.BitDepth == 4)
            {
                // each pixel of byte suppose to be 2 pixels
                throw new Exception();
            }
            else if (HeaderDetails.BitDepth == 8)
            {
                if (writingIndex >= result.Count)
                    return;

                result[writingIndex++] = _colorTable!.Value[item];

            }
        }

    }

}
