using ImageQT.DllInterop.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Mac;
internal static class NSContentView
{
    public static void AddSubview(this IntPtr contentView, NSImageView nsImageView)
    {
        var selector = ObjectCRuntime.SelGetUid("addSubview:");
        ObjectCRuntime.ObjCMsgSend(contentView, selector, nsImageView);
    }
}
