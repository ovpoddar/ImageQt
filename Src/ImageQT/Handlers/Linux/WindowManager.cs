#if DEBUG || Linux
using Xcsb.Connection;
using Xcsb;
using Xcsb.Connection.Models.Handshake;
using Xcsb.Infrastructure;
using Xcsb.Masks;
using Xcsb.Models;
using Xcsb.Models.TypeInfo;
using Xcsb.Response.Event;

namespace ImageQT.Handlers.Linux;

public class WindowManager : INativeWindowManager
{
    private readonly IXConnection _xConnection;
    private readonly IXProto _xProto;
    private readonly Screen _screen;

    private uint _window;
    private uint _gc;
    private uint _pixmap;

    public WindowManager()
    {
        _xConnection = XcsbClient.Connect();

        if (_xConnection.HandshakeSuccessResponseBody == null)
            throw new Exception(_xConnection.FailReason);

        _xProto = _xConnection.Initialized();
        _screen = _xConnection.HandshakeSuccessResponseBody.Screens[0];
    }

    public void CreateWindow(uint height, uint width)
    {
        _window = _xConnection.NewId();
        _xProto.CreateWindowChecked(
            0,
            _window,
            _screen.Root,
            0, 0, (ushort)width, (ushort)height, 0,
            ClassType.InputOutput,
            _screen.RootVisualId,
            ValueMask.EventMask,
            [(uint)EventMask.ExposureMask]
        );
    }

    public void SetUpImage(Image image)
    {
        _gc = _xConnection.NewId();
        _xProto.CreateGCChecked(_gc, _window, 0, []);

        _pixmap = _xConnection.NewId();
        _xProto.CreatePixmapChecked(_screen.RootDepth.DepthValue, _pixmap, _window, (ushort)image.Width,
            (ushort)image.Height);
        unsafe
        {
            var data = new Span<byte>((void*)image.Id.AddrOfPinnedObject(), (image.Width * image.Height * 4));
            _xProto.PutImageChecked(ImageFormatBitmap.ZPixmap, _pixmap, _gc, (ushort)image.Width,
                (ushort)image.Height, 0, 0, 0, _screen.RootDepth.DepthValue, data);
        }
    }

    public Task Show(DateTime? closeTime = null)
    {
        _xProto.MapWindowChecked(_window);
        while (true)
        {
            if (closeTime != null && closeTime.Value < DateTime.Now)
                break;
            var hasEventToProcesses = _xProto.IsEventAvailable();
            if (hasEventToProcesses) continue;
            var evnt = _xProto.GetEvent();
            if (evnt.ReplyType == EventType.LastEvent) break;
            if (evnt.ReplyType == EventType.Expose)
            {
                var expose = evnt.As<ExposeEvent>();
                _xProto.CopyAreaChecked(_pixmap,
                    _window,
                    _gc,
                    expose.X, expose.Y,
                    expose.X, expose.Y,
                    expose.Width, expose.Height);
            }
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _xProto.FreePixmapChecked(_pixmap);
        _xProto.FreeGCChecked(_gc);
        _xProto.DestroyWindowChecked(_window);
        _xConnection.Dispose();
    }
}
#endif