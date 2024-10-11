using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Decoder.BMP.Models.Feature;
internal struct RLEPositionTracker
{
    private readonly bool _isTopToBottom;
    private readonly int _width;
    private readonly int _height;

    public RLEPositionTracker(bool isTopToBottom, int width, int height)
    {
        _isTopToBottom = isTopToBottom;
        _width = width;
        _height = height;
        SetWithXYValue(0, 0);
    }

    public long Position { get; private set; }
    public int XWWidth { get; private set; }
    public int YHHeight { get; private set; }

    public void SetWithXYValue(int x, int y)
    {
        XWWidth = x;
        YHHeight = GetNormalizeYPosition(_isTopToBottom, _height, y);
        Position = YHHeight * _width + XWWidth;
    }

    public void SetWithPosition(int position)
    {
        Position += position;
        XWWidth = (int)(Position % _width);
        YHHeight = (int)(Position / _width);
    }


    private static int GetNormalizeYPosition(bool isTopToBottom, int height, int position) =>
        isTopToBottom
            ? position
            : height - 1 - position;

}
