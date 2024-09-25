using ImageQT.Models.ImagqQT;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class GrayScaleColorConverter : BaseRGBColorConverter
{
    private readonly byte? _mask;
    private readonly byte? _map;
    public GrayScaleColorConverter(IHDRData headerData) : base(headerData)
    {
        if (HeaderData.BitDepth < 8)
            (_mask, _map) = BitDepthDetailsForGrayScale();
    }

    internal override void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex)
    {

        if (HeaderData.BitDepth < 8
            && _mask.HasValue
            && _map.HasValue)
        {
            Debug.Assert(currentByte.Length == 1);
            for (var j = 0; j < 8; j += HeaderData.BitDepth)
            {
                var currentBit = (byte)((byte)(currentByte[0] >> 8 - HeaderData.BitDepth - j & _mask.Value) * (255 / _map.Value));
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
