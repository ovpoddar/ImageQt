using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Mac;

[Flags]
public enum NSBackingStore : ulong
{
    Buffered = 2,
}
