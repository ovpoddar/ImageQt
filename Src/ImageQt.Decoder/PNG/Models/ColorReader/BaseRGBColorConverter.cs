// Ignore Spelling: Palated

using ImageQT.Decoder.Models;

namespace ImageQT.Decoder.PNG.Models.ColorReader;
internal abstract class BaseRGBColorConverter
{
    public readonly IHDRData HeaderData;

    protected BaseRGBColorConverter(IHDRData headerData) =>
        HeaderData = headerData;

    internal abstract void Write(ArraySegment<Pixels> result, Span<byte> currentByte, ref int writingIndex);

    // TODO:OPTAMICE: simplify make the non loop one 
    protected (byte step, byte mask) BitDepthDetailsForPalated()
    {
        byte step = 1;
        for (byte i = 0; i < HeaderData.BitDepth; i++)
            step |= (byte)(1 << i);

        return ((byte)(8 - HeaderData.BitDepth), step);
    }

    protected (byte mask, byte map) BitDepthDetailsForGrayScale() =>
        ((byte)(0xFF >> 8 - HeaderData.BitDepth), (byte)((1 << HeaderData.BitDepth) - 1));
}
