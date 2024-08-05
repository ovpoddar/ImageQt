using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.PNG.Models;
internal readonly struct PLTEData
{
    public readonly byte[] Palette;
    public PLTEData(PNGChunk palate)
    {
        Palette = new byte[palate.Length];
        palate.GetData(Palette);

        Debug.Assert(palate.Length % 3 == 0);
        for (var i = 0; i < Palette.Length; i+=3)
        {
            var responce = new Span<byte>(Palette, i, 3);
            responce.Reverse();
        }
    }

    // b,g,r format
    public readonly ReadOnlySpan<byte> this[int index] => 
        new Span<byte>(Palette, index *= 3, 3);
}
