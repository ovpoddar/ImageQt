﻿using ImageQT.Decoder;

namespace ImageQT.Handlers.ImageQT;
internal class SupportedImageDecoder
{
    internal static IEnumerable<IImageDecoder> GetSupportedDecoders(Stream stream)
    {
        var types = typeof(SupportedImageDecoder).Assembly.GetTypes()
            .Where(a => a.IsClass
                && a.GetInterfaces().Contains(typeof(IImageDecoder)))
            .Select(a => Activator.CreateInstance(a, stream))
            .Cast<IImageDecoder>();
        return types;
    }

}
