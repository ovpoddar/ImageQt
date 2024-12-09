using ImageQT.Decoder.BMP;
using ImageQT.Decoder.PNG;

namespace ImageQT.Decoder;
internal class SupportedImageDecoder
{
    internal static IEnumerable<IImageDecoder> GetSupportedDecoders(Stream stream)
    {
#if AOT
        var types = new List<IImageDecoder>()
        {
            new BmpDecoder(stream),
            new PngDecoder(stream)
        };
#else
         var types = typeof(SupportedImageDecoder).Assembly.GetTypes()
                    .Where(a => a.IsClass
                        && a.GetInterfaces().Contains(typeof(IImageDecoder)))
                    .Select(a => Activator.CreateInstance(a, stream))
                    .Cast<IImageDecoder>();
#endif
        return types;
    }

}
