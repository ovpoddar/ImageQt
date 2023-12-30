namespace ImageQt.Models.Linux;

[Flags]
public enum EventMask : long
{
    NoEventMask = 0L,
    KeyPressMask = 1L << 0,
    KeyReleaseMask = 1L << 1,
    ButtonPressMask = 1L << 2,
    ButtonReleaseMask = 1L << 3,
    EnterWindowMask = 1L << 4,
    LeaveWindowMask = 1L << 5,
    PointerMotionMask = 1L << 6,
    PointerMotionHintMask = 1L << 7,
    Button1MotionMask = 1L << 8,
    Button2MotionMask = 1L << 9,
    Button3MotionMask = 1L << 10,
    Button4MotionMask = 1L << 11,
    Button5MotionMask = 1L << 12,
    ButtonMotionMask = 1L << 13,
    KeymapStateMask = 1L << 14,
    ExposureMask = 1L << 15,
    VisibilityChangeMask = 1L << 16,
    StructureNotifyMask = 1L << 17,
    ResizeRedirectMask = 1L << 18,
    SubstructureNotifyMask = 1L << 19,
    SubstructureRedirectMask = 1L << 20,
    FocusChangeMask = 1L << 21,
    PropertyChangeMask = 1L << 22,
    ColormapChangeMask = 1L << 23,
    OwnerGrabButtonMask = 1L << 24,
}
