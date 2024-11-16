using ImageQT.Decoder.Models;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class RGBAColorConverter : BaseRGBColorConverter
{
    public RGBAColorConverter(IHDRData headerData) : base(headerData) { }

    internal override void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex)
    {
        if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 4);
            result[writingIndex++] = new Pixels(currentByte[0],
                currentByte[1],
                currentByte[2],
                currentByte[3]);
        }
        else // 16
        {
            Debug.Assert(currentByte.Length == 8);
            result[writingIndex++] = new Pixels(currentByte[0],
                currentByte[2],
                currentByte[4],
                currentByte[6]);
        }
    }
}
