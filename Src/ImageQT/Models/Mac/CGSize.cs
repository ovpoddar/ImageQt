﻿#if DEBUG || OSX
namespace ImageQT.Models.Mac;

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
#endif
