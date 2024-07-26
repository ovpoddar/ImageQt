using ImageQT.Exceptions;
using System.Runtime.InteropServices;

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
