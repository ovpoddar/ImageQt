using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.JPG;
internal class JpgDecoder : IImageDecoder
{
    private readonly Stream _fileStream;

    public JpgDecoder(Stream stream) =>
        _fileStream = stream;

    public bool CanProcess()
    {
        return false;
    }

    public Image Decode()
    {
        throw new NotImplementedException();
    }
}
