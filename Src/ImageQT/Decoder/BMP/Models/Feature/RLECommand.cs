﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal struct RLECommand
{
    public RLECommandType CommandType { get; set; }
    public byte Data1 { get; set; }
    public byte Data2 { get; set; }

}
