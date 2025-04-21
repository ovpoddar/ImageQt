using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux._private;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WireVec
{
    // repeat this 128 times
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector0;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector1;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector2;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector3;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector4;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector5;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector6;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector7;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector8;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector9;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector10; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector11; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector12; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector13; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector14; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector15; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector16; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector17; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector18; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector19; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector20; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector21; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector22; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector23; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector24; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector25; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector26; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector27; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector28; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector29; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector30; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector31; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector32; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector33; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector34; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector35; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector36; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector37; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector38; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector39; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector40; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector41; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector42; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector43; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector44; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector45; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector46; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector47; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector48; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector49; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector50; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector51; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector52; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector53; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector54; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector55; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector56; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector57; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector58; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector59; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector60; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector61; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector62; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector63; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector64; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector65; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector66; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector67; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector68; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector69; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector70; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector71; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector72; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector73; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector74; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector75; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector76; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector77; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector78; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector79; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector80; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector81; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector82; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector83; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector84; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector85; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector86; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector87; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector88; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector89; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector90; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector91; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector92; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector93; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector94; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector95; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector96; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector97; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector98; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector99; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector100;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector101;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector102;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector103;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector104;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector105;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector106;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector107;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector108;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector109;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector110;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector111;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector112;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector113;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector114;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector115;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector116;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector117;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector118;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector119;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector120;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector121;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector122;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector123;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector124;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector125;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector126;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> WireVector127;  // dpy, re, event
}

