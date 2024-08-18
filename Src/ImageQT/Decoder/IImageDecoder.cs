using ImageQT.Models.ImagqQT;

namespace ImageQT.Decoder;
internal interface IImageDecoder
{
    bool CanProcess();
    Image Decode();
}
