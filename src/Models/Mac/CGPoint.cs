﻿namespace ImageQt.Models.Mac;
public struct CGPoint
{
    public double X;

    public double Y;

    public CGPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public CGPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

}

