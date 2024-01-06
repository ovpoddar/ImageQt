#if Windows
global using Windows = ImageQt.Handler.Windows.Window;
#elif Linux
global using Windows = ImageQt.Handler.Linux.Window;
#elif OSX
global using Windows = ImageQt.Handler.Mac.Window;
#endif