using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RleColorReader : BaseColorReader
{
    public RleColorReader(BMPHeader header) : base(header) {}

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        //TODO:INVISTAGATE:: can i decode rle on go rather all at once.
        throw new NotImplementedException();
    }
}
