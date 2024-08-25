// Ignore Spelling: Rgb

using ImageQT.Decoder.BMP.Models.DIbFileHeader;
using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal class RgbColorReader : BaseColorReader
{
    public RgbColorReader(Stream fileStream, RequiredProcessData RequiredProcessData)
        : base(fileStream, RequiredProcessData) { }

    internal override void Decode(Pixels[] result)
    {
        // TODO: 1, 4, 8, 16, 24, 32
    }
}
