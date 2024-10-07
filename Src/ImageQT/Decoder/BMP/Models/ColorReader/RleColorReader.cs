using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RleColorReader : BaseColorReader
{
    private readonly ColorTable? _colorTable;

    public override bool IsRLE => true;
    public RleColorReader(BMPHeader header, ColorTable? colorTable) : base(header)
    {
        _colorTable = colorTable;
    }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex, bool isUndefinedPixels)
    {
        for (int i = 0; i < pixel.Length; i++)
        {
            var item = pixel[i];
            if (HeaderDetails.BitDepth == 4)
            {
                // each pixel of byte suppose to be 2 pixels
                throw new Exception();
            }
            else if (HeaderDetails.BitDepth == 8)
            {
                //TODO:INVISTAGATE: their are other approaches, not sure which one to pick
                // no documentation found about this. may be missing something check the chromium 
                // codes once again.
                if (isUndefinedPixels)
                    result[writingIndex++] = new Pixels(); // _colorTable!.Value[0];
                else
                    result[writingIndex++] = _colorTable!.Value[item];

            }
        }
    }

}
