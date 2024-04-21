// Ignore Spelling: Mipmaps

namespace ImageQT.Models.ImagqQT;

public readonly struct Image
{
    public required nint Id { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int Mipmaps { get; init; }
    public required int Format { get; init; }
}
