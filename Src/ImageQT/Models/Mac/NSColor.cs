#if DEBUG || OSX
using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal struct NSColor
{
    public IntPtr _stackPtr;
    public NSColor(NSImage patternImage)
    {
        var colorClass = ObjectCRuntime.ObjCGetClass("NSColor");
        var selector = ObjectCRuntime.SelGetUid("colorWithPatternImage:");
        _stackPtr = ObjectCRuntime.PointerObjCMsgSend(colorClass, selector, patternImage);
    }

    public static implicit operator IntPtr(NSColor color) => 
        color._stackPtr;
}
#endif
