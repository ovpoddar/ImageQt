#if DEBUG || OSX
namespace ImageQT.Models.Mac;
internal enum NSBackingStore : ulong
{
    Retained,
    Nonretained,
    Buffered
}
#endif
