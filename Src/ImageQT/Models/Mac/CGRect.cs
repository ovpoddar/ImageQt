using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal struct CGRect
{
    public CGPoint Origin;

    public CGSize Size;

    public CGPoint Location
    {
        get
        {
            return Origin;
        }
        set
        {
            Origin = value;
        }
    }

    public double Left => X;

    public double Top => Y;

    public double Right => X + Width;

    public double Bottom => Y + Height;

    public double X
    {
        get
        {
            return Origin.X;
        }
        set
        {
            Origin.X = value;
        }
    }

    public double Y
    {
        get
        {
            return Origin.Y;
        }
        set
        {
            Origin.Y = value;
        }
    }

    public double Width
    {
        get
        {
            return Size.Width;
        }
        set
        {
            Size.Width = value;
        }
    }

    public double Height
    {
        get
        {
            return Size.Height;
        }
        set
        {
            Size.Height = value;
        }
    }

    public CGRect(CGPoint location, CGSize size)
    {
        Origin.X = location.X;
        Origin.Y = location.Y;
        Size.Width = size.Width;
        Size.Height = size.Height;
    }

    public CGRect(double x, double y, double width, double height)
    {
        Origin.X = x;
        Origin.Y = y;
        Size.Width = width;
        Size.Height = height;
    }

}
