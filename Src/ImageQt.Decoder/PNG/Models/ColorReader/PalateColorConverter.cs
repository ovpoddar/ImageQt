using ImageQT.Decoder.Models;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class PalateColorConverter : BaseRGBColorConverter
{
    private readonly PLTEData _palate;
    private readonly byte? _mask;
    private readonly byte? _step;

    public PalateColorConverter(PLTEData palate, IHDRData headerData) : base(headerData)
    {
        _palate = palate;
        if (HeaderData.BitDepth < 8)
            (_mask, _step) = BitDepthDetailsForPalated();
    }

    internal override void Write(ArraySegment<Pixels> result,
        Span<byte> currentByte,
        ref int writingIndex)
    {

        if (HeaderData.BitDepth < 8
            && _step.HasValue
            && _mask.HasValue)
        {
            Debug.Assert(currentByte.Length == 1);
            // less than 8 n
            // TODO:INVESTIGATION: may be need to make it reverce too for little endien
            for (int j = _step.Value; j >= 0; j -= HeaderData.BitDepth)
            {
                var mask = (byte)(_mask.Value << j);
                var currentBit = (byte)((currentByte[0] & mask) >> j);
                var colors = _palate[currentBit];

                if (writingIndex < HeaderData.Width)
                    result[writingIndex++] = colors;
            }
        }
        else if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 1);
            result[writingIndex++] = _palate[currentByte[0]];
        }
    }
}
