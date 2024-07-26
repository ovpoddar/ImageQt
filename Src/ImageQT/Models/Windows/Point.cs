#if DEBUG || Windows
namespace ImageQT.Models.Windows;
internal struct Point
{
    public long X { get; set; }
    public long Y { get; set; }
}
#endif