using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP;
internal class BmpDecoder : IImageDecoder
{
    private readonly Stream _fileStream;

    public BmpDecoder(Stream stream) =>
        _fileStream = stream;

    public bool CanProcess()
    {
        throw new NotImplementedException();
    }

    public Image Decode()
    {
        throw new NotImplementedException();
    }
}
