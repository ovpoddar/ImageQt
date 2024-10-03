﻿using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RleColorReader : BaseColorReader
{
    public RleColorReader(BMPHeader header) : base(header) { }

    internal override void Decode(ArraySegment<Pixels> result, Span<byte> pixel, ref int writingIndex)
    {
        if (HeaderDetails.BitDepth == 4)
        {

        }
        else if (HeaderDetails.BitDepth == 8)
        {
            
        }
    }
}
