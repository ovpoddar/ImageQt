using ImageQT.DllInterop.Windows;
using ImageQT.Models.ImagqQT;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Windows;
internal class WindowWrapper : SafeHandleZeroInvalid
{
    private WindowWrapper() : base(true) { }

    protected override bool ReleaseHandle() =>
        User32.DestroyWindow(handle);
}