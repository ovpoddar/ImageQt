namespace ImageQt.Models.Mac;

[Flags]
public enum NSWindowStyleMask : ulong
{
    Borderless = 0,
    Titled = 1,
    Closable = 2,
    Miniaturizable = 4,
    Resizable = 8,
}
