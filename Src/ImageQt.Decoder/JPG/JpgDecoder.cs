using ImageQT.Models.ImagqQT;

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
