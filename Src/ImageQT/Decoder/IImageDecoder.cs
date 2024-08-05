using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder;
internal interface IImageDecoder
{
    bool CanProcess();
    Image Decode();
}
