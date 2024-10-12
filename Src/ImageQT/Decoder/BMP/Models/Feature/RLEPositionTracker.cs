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

    public void SetWithPositionAsRelative(int position)
    {
        XWWidth = (int)(position % _width);
        YHHeight = (int)(position / _width);
        Position += position;
    }


    public void SetWithXYAsRelative(int x, int y)
    {
        XWWidth += x;
        YHHeight = GetNormalizeYPosition(_isTopToBottom, _height, YHHeight + y);
        Position = YHHeight * _width + XWWidth;
    }
    private static int GetNormalizeYPosition(bool isTopToBottom, int height, int position) =>
        isTopToBottom
            ? position
            : height - 1 - position;

}
