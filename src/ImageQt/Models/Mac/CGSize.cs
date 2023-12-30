using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Models.Mac;
public struct CGSize
{

    public double Width;

    public double Height;

    public CGSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

}
