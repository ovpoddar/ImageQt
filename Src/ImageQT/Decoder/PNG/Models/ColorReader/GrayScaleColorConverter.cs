using ImageQT.Models.ImagqQT;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class GrayScaleColorConverter : BaseRGBColorConverter
{
    public GrayScaleColorConverter(IHDRData headerData) : base(headerData) { }

    internal override void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex)
    {
        var bitDetails = BitDepthDetailsForGrayScale();
        if (bitDetails is { mask: not null, map: not null })
        {
            Debug.Assert(currentByte.Length == 1);
            // TODO: may be need to make it reverce too for little endien
            for (var j = 0; j < 8; j += HeaderData.BitDepth)
            {
                var currentBit = (byte)((byte)(currentByte[0] >> 8 - HeaderData.BitDepth - j & bitDetails.mask) * (255 / bitDetails.map));
                if (writingIndex < HeaderData.Width)
                    result[writingIndex++] = new Pixels(currentBit, currentBit, currentBit);
            }
        }
        else if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 1);
            result[writingIndex++] = new Pixels(currentByte[0], currentByte[0], currentByte[0]);
        }
        else // 16
        {
            Debug.Assert(currentByte.Length == 2);
            result[writingIndex++] = new Pixels(currentByte[0], currentByte[0], currentByte[0]);
        }
    }
}
