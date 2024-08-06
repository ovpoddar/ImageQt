using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.PNG.Models;
internal readonly struct PLTEData
{
    public readonly Pixels[] Palette;
    public PLTEData(PNGChunk palate)
    {
        Debug.Assert(palate.Length % 3 == 0);
        Palette = new Pixels[palate.Length / 3];
        Span<byte> palette = stackalloc byte[(int)palate.Length];
        palate.GetData(palette);

        for (var i = 0; i < Palette.Length; i++)
        {
            var index = i * 3;
            Palette[i] = new Pixels(palette[index], palette[index + 1], palette[index + 2]);
        }
    }

    public readonly Pixels this[int index] =>
        Palette[index];
}
