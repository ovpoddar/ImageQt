using ImageQT.Models.ImagqQT;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class GreyScaleAndAlphaConverter : BaseRGBColorConverter
{
    public GreyScaleAndAlphaConverter(IHDRData headerData) : base(headerData) { }

    internal override void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex)
    {
        if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 2);
            result[writingIndex++] = new Pixels(currentByte[0],
                currentByte[0],
                currentByte[0],
                currentByte[1]);

        }
        else // 16
        {
            Debug.Assert(currentByte.Length == 4);
            result[writingIndex++] = new Pixels(currentByte[0],
                currentByte[0],
                currentByte[0],
                currentByte[2]);
        }
    }
}
