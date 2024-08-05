﻿using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class PalateColorConverter : BaseRGBColorConverter
{
    private readonly PLTEData _palate;

    public PalateColorConverter(PLTEData palate, IHDRData headerData) : base(headerData) =>
        _palate = palate;

    internal override void Write(ArraySegment<byte> result,
        Span<byte> currentByte,
        ref int writingIndex)
    {
        var bitDetails = BitDepthDetailsForPalated();
        if (bitDetails is { mask: not null, step: not null })
        {
            Debug.Assert(currentByte.Length == 1);
            // less than 8 n
            for (int j = bitDetails.step!.Value; j >= 0; j -= HeaderData.BitDepth)
            {
                var mask = (byte)(bitDetails.mask << j);
                var currentBit = (byte)((currentByte[0] & mask) >> j);
                var colors = _palate[currentBit];

                if (writingIndex < HeaderData.Width * 4)
                {
                    for (var i = 0; i < colors.Length; i++)
                        result[writingIndex++] = colors[i];
                    // for alpha
                    result[writingIndex++] = 255;
                }
            }
        }
        else if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 1);
            result[writingIndex++] = _palate[currentByte[0]][0];
            result[writingIndex++] = _palate[currentByte[0]][1];
            result[writingIndex++] = _palate[currentByte[0]][2];
            result[writingIndex++] = 255;
        }
    }
}
