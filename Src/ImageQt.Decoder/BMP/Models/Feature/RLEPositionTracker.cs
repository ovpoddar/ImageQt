﻿namespace ImageQT.Decoder.BMP.Models.Feature;
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

    public readonly int GetCurrentX() =>
        (int)(Position % _width);

    public readonly int GetCurrentY() =>
        (int)(Position / _width);

    public void SetWithPositionAsAbsolute(long position) =>
        Position = position;

    public void UpdatePositionToNextRowStart(int addDefault = 0)
    {
        Position += addDefault;
        Position = (_isTopToBottom ? GetCurrentY() + 1 : GetCurrentY() - 1) * _width;
    }

    public void SetWithXYAsRelative(int x, int y)
    {
        var nx = GetCurrentX() + x;
        var ny = _isTopToBottom ? GetCurrentY() + y : GetCurrentY() - y;
        var newPosition = ny * _width + nx;
        SetWithPositionAsAbsolute(newPosition);
    }

}
