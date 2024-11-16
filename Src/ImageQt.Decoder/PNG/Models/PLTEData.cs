using ImageQT.Decoder.Models;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models;
internal readonly struct PLTEData
{
    private readonly Pixels[] _palette;
    public PLTEData(PNGChunk palate)
    {
        Debug.Assert(palate.Length % 3 == 0);
        _palette = new Pixels[palate.Length / 3];
        Span<byte> palette = stackalloc byte[(int)palate.Length];
        palate.GetData(palette);

        for (var i = 0; i < _palette.Length; i++)
        {
            var index = i * 3;
            _palette[i] = new Pixels(palette[index], palette[index + 1], palette[index + 2]);
        }
    }

    public readonly Pixels this[int index] =>
        _palette[index];
}
