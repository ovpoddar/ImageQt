using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.DIbFileHeader;

public enum HeaderCompression : uint
{
    Rgb = 0,
    Rle8 = 1,
    Rle4 = 2,
    BitFields = 3,
    Jpeg = 4,
    Png = 5,
    AlphaBitFields = 6,
    CMYK = 11,
    CMYKrle8 = 12,
    CMYKrle4 = 13
}