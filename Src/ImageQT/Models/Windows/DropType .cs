#if DEBUG || Windows
namespace ImageQT.Models.Windows;
internal enum DropType : uint
{
    SrcCopy = 0x00CC0020
}
#endif