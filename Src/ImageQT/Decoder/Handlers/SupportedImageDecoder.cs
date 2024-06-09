using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.Handlers;
internal static class SupportedImageDecoder
{
    internal static IEnumerable<IImageDecoder> GetSupportedDecoders()
    {
        var types = typeof(SupportedImageDecoder).Assembly.GetTypes()
            .Where(a => a.IsClass
                && a.GetInterfaces().Contains(typeof(IImageDecoder)))
            .Select(a => Activator.CreateInstance(a))
            .Cast<IImageDecoder>();
        return types;
    }

    internal static Image? GetImage(this IImageDecoder imageDecoder, Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        Span<byte> headerBytes = stackalloc byte[imageDecoder.RequiredByteToRead];
        var header = stream.Read(headerBytes);

        Debug.Assert(header == imageDecoder.RequiredByteToRead);

        if (!imageDecoder.IsSupport(headerBytes))
            return null;

        stream.Seek(0, SeekOrigin.Begin);
        var (width, height, bitCount) = imageDecoder.GetImageDetails(stream);

        stream.Seek(0, SeekOrigin.Begin);
        var bytes = imageDecoder.DecodeImage(stream);
        var imageData = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);

        return new Image()
        {
            Height = (int)height,
            Width = (int)width,
            Id = imageData,
            Format = 1,
            Mipmaps = 1,
            BitCount = bitCount
        };
    }
}
