#if Windows
global using Windows = ImageQt.Handler.Windows.Window;
#elif Linux
using Windows = ImageQt.Handler.Linux.Window;
#elif OSX
using Windows = ImageQt.Handler.Mac.Window;
#endif