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
        SetWithPositionAsAbsolute(_isTopToBottom ? 0 : _width * (_height - 1));
    }

    public long Position { get; private set; }
    public int XWWidth { get; private set; }
    public int YHHeight { get; private set; }

    public void SetWithPositionAsRelative(int position)
    {
        Position += position;
        XWWidth = (int)(Position % _width);
        YHHeight = (int)(Position / _width);
    }

    public void SetWithPositionAsAbsolute(int position)
    {
        Position = position;
        XWWidth = position % _width;
        YHHeight = position / _width;
    }
    public void UpdatePositionToNextRowStart()
    {
        XWWidth = 0;
        if (_isTopToBottom)
            YHHeight++;
        else
            YHHeight--;
        Position = YHHeight * _width + XWWidth;
    }

    public void SetWithXYAsRelative(int x, int y)
    {
        XWWidth += x;
        YHHeight = y == 0 ? YHHeight : GetNormalizeYPosition(_isTopToBottom, _height, (YHHeight + y));
        Position = YHHeight * _width + XWWidth;
    }

    private static int GetNormalizeYPosition(bool isTopToBottom, int height, int position) =>
        isTopToBottom
            ? position
            : height - 1 - position;

}
