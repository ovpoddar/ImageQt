using ImageQT.Decoder.BMP;
using ImageQT.Decoder.PNG;

namespace ImageQT.Decoder;
internal class SupportedImageDecoder
{
    internal static IEnumerable<IImageDecoder> GetSupportedDecoders(Stream stream)
    {
        var types = new List<IImageDecoder>()
        {
            new BmpDecoder(stream),
            new PngDecoder(stream)
        };
        return types;
    }

}
