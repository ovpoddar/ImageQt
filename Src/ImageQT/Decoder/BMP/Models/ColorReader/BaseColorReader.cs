using ImageQT.Models.ImagqQT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.ColorReader;
internal abstract class BaseColorReader
{
    public Stream FileStream { get; }
    public RequiredProcessData RequiredProcessData { get; }

    protected BaseColorReader(Stream fileStream, RequiredProcessData RequiredProcessData)
    {
        this.FileStream = fileStream;
        this.RequiredProcessData = RequiredProcessData;
    }

    internal abstract void Decode(Pixels[] result);
}
