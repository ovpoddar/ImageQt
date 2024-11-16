namespace ImageQT.Decoder.PNG.Models.Filters;
public class BasePNGFilter
{
    private readonly Stream _stream;

    public BasePNGFilter(Stream stream) =>
        _stream = stream;

    internal virtual Span<byte> UnApply(Span<byte> currentPixel, int scanlineWidth)
    {
        _stream.Seek(-currentPixel.Length, SeekOrigin.Current);
        _stream.Write(currentPixel);
        return currentPixel;
    }

    protected void GetTopPixel(Span<byte> result, int scanLineWidth)
    {
        if (_stream.Position <= scanLineWidth + 1)
            return;

        var tempPosation = _stream.Position;
        _stream.Seek(-(result.Length + scanLineWidth), SeekOrigin.Current);
        _stream.Read(result);
        _stream.Position = tempPosation;
    }

    protected void GetTopLeftPixel(Span<byte> result, int scanLineWidth)
    {
        var currentPosition = _stream.Position;
        var pixelLength = result.Length;
        var modPosation = _stream.Position % scanLineWidth;
        if (modPosation <= pixelLength + 1 && modPosation > 0 || currentPosition <= scanLineWidth + 1)
            return;

        _stream.Seek(-(2 * pixelLength + scanLineWidth), SeekOrigin.Current);
        _stream.Read(result);
        _stream.Position = currentPosition;
    }
}
