using ImageQT.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.ImagqQT;
internal class SafeHandleZeroInvalid : SafeHandle
{
    public SafeHandleZeroInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) { }

    public SafeHandleZeroInvalid(IntPtr handler) : base(IntPtr.Zero, true) =>
        SetHandle(handler);

    public override bool IsInvalid =>
        handle == IntPtr.Zero;

    protected override bool ReleaseHandle() =>
        throw new ShouldNotBeCalledException();
}
