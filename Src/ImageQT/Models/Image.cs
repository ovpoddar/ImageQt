﻿// Ignore Spelling: Mipmaps

namespace ImageQT;

public readonly struct Image
{
    public required IntPtr Id { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int Mipmaps { get; init; }
    public required int Format { get; init; }
}
