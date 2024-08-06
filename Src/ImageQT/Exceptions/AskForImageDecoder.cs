using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Exceptions;
public class AskForImageDecoder : Exception
{
    public AskForImageDecoder() : base("looks like this type of image is not supported. What to do if you understand the file type try to implement it make a pr.")
    {

    }
}
