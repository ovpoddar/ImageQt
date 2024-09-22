﻿using ImageQT.Models.ImagqQT;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class PalateColorConverter : BaseRGBColorConverter
{
    private readonly PLTEData _palate;

    public PalateColorConverter(PLTEData palate, IHDRData headerData) : base(headerData) =>
        _palate = palate;

    internal override void Write(ArraySegment<Pixels> result,
        Span<byte> currentByte,
        ref int writingIndex)
    {
        var bitDetails = BitDepthDetailsForPalated(); // TODO: MIGHT NOT NEED TO CALCULATE ON EVERY WRITE
        if (bitDetails is { mask: not null, step: not null })
        {
            Debug.Assert(currentByte.Length == 1);
            // less than 8 n
            // TODO: may be need to make it reverce too for little endien
            for (int j = bitDetails.step!.Value; j >= 0; j -= HeaderData.BitDepth)
            {
                var mask = (byte)(bitDetails.mask << j);
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
