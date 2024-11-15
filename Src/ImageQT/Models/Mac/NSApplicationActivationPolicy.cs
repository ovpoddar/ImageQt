#if DEBUG || OSX
namespace ImageQT.Models.Mac;
internal enum NSApplicationActivationPolicy : int
{
    Regular,
    Accessory,
    Prohibited
}
#endif