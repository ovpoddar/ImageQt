using ImageQT.Models.ImagqQT;
using System.Diagnostics;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal class RGBColorConverter : BaseRGBColorConverter
{

    public RGBColorConverter(IHDRData headerData) : base(headerData) { }

    internal override void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex)
    {
        if (HeaderData.BitDepth == 8)
        {
            Debug.Assert(currentByte.Length == 3);
            result[writingIndex++] = new Pixels(currentByte[0], currentByte[1], currentByte[2]);
        }
        // 16
        else
        {
            Debug.Assert(currentByte.Length == 6);
            result[writingIndex++] = new Pixels(currentByte[0], currentByte[2], currentByte[4]);
        }
    }
}
