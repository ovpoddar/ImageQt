using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal enum NSBackingStore : ulong
{
    Retained,
    Nonretained,
    Buffered
}

