// Ignore Spelling: Palated

using ImageQT.Models.ImagqQT;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal abstract class BaseRGBColorConverter
{
    public readonly IHDRData HeaderData;

    protected BaseRGBColorConverter(IHDRData headerData) =>
        HeaderData = headerData;

    internal abstract void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex);
    
    // TODO: simplify make the non loop one 
    protected (byte? step, byte? mask) BitDepthDetailsForPalated()
    {
        if (HeaderData.BitDepth < 8)
        {
            byte step = 1;
            for (byte i = 0; i < HeaderData.BitDepth; i++)
                step |= (byte)(1 << i);

            return ((byte)(8 - HeaderData.BitDepth), step);
        }
        return (null, null);
    }

    protected (byte? mask, byte? map) BitDepthDetailsForGrayScale()
    {
        if (HeaderData.BitDepth < 8)
            return ((byte)(0xFF >> 8 - HeaderData.BitDepth), (byte)((1 << HeaderData.BitDepth) - 1));
        return (null, null);
    }
}
