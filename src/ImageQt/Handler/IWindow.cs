using ImageQt.Models.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQt.Handler;
internal interface IWindow
{
    void CleanUpResources(ref IntPtr window);
    nint DeclareWindow(string windowTitle, uint height, uint width);
    void ProcessEvent(nint window);
    void ShowWindow(nint window);
    void LoadBitMap(int width, int height, ref IntPtr imageData, IntPtr display);
}
