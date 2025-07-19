#if DEBUG || Linux
using Xcsb;
using Xcsb.Masks;
using Xcsb.Models;
using Xcsb.Models.Event;

namespace ImageQT.Handlers.Linux;
internal class WindowManager : INativeWindowManager
{
    private readonly IXProto _xProto;
    private readonly uint _windowId;
    private readonly uint _gc;
    private bool _connected;
    private IntPtr _imagePtr;
    private ushort _imgWidth;
    private ushort _imgHeight;

    public WindowManager()
    {
        _xProto = XcsbClient.Initialized();
        _windowId = _xProto.NewId();
        _gc = _xProto.NewId();
    }

    public unsafe void CreateWindow(uint height, uint width)
    {
        var screen = _xProto.HandshakeSuccessResponseBody.Screens[0];
        _xProto.BufferClient.CreateWindow(screen.RootDepth!.DepthValue,
            _windowId,
            screen.Root,
            0, 0, (ushort)width, (ushort)height,
            0, Xcsb.Models.ClassType.InputOutput,
            screen.RootVisualId,
            ValueMask.EventMask,
            [(uint)(EventMask.ExposureMask)]);
        _xProto.BufferClient.CreateGC(_gc,
            _windowId,
            GCMask.Foreground | GCMask.GraphicsExposures,
            [screen.BlackPixel, 0]);
        _xProto.BufferClient.MapWindow(_windowId);
        _xProto.BufferClient.CreateGC(_gc, _windowId, GCMask.Foreground | GCMask.GraphicsExposures, [screen.BlackPixel, 0]);
        _xProto.BufferClient.FlushChecked();
    }

    public void Dispose()
    {
        if (_connected)
        {
            _xProto.FreeGC(_gc);
            _xProto.DestroyWindow(_windowId);
        }
    }

    public unsafe Task Show(DateTime? closeTime = null)
    {
        var data = new Span<byte>((void*)_imagePtr, _imgWidth * _imgHeight * 4);
        while (true)
        {
            var evnt = _xProto.GetEvent();
            if (!evnt.HasValue || closeTime != null && closeTime.Value < DateTime.Now)
            {
                _connected = false;
                break;
            }
            if (evnt.Value.EventType == EventType.Expose)
            {
                // todo: hack to big request or write it with lazy request
                //_xProto.PutImageChecked(ImageFormat.ZPixmap,
                //   _windowId,
                //   _gc,
                //   _imgWidth,
                //   _imgHeight,
                //   0, 0, 0,
                //    _xProto.HandshakeSuccessResponseBody.Screens[0].RootDepth!.DepthValue,
                //   data);

            }
            if (evnt.Value.EventType == EventType.Error)
                break;
        }
        return Task.CompletedTask;
    }

    public void SetUpImage(Image image)
    {
        _imagePtr = image.Id.AddrOfPinnedObject();
        _imgWidth = (ushort)image.Width;
        _imgHeight = (ushort)image.Height;
    }
}
#endif