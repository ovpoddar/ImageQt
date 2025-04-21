using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.Models.Linux._private;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EventVec
{
    // repeat this 128 times
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector0;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector1;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector2;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector3;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector4;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector5;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector6;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector7;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector8;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector9;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector10;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector11; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector12; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector13; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector14; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector15; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector16; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector17; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector18; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector19; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector20; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector21; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector22; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector23; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector24; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector25; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector26; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector27; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector28; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector29; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector30; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector31; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector32; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector33; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector34; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector35; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector36; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector37; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector38; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector39; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector40; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector41; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector42; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector43; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector44; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector45; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector46; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector47; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector48; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector49; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector50; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector51; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector52; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector53; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector54; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector55; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector56; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector57; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector58; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector59; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector60; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector61; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector62; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector63; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector64; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector65; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector66; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector67; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector68; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector69; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector70; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector71; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector72; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector73; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector74; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector75; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector76; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector77; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector78; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector79; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector80; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector81; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector82; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector83; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector84; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector85; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector86; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector87; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector88; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector89; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector90; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector91; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector92; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector93; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector94; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector95; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector96; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector97; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector98; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector99; // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector100;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector101;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector102;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector103;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector104;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector105;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector106;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector107;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector108;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector109;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector110;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector111;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector112;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector113;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector114;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector115;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector116;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector117;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector118;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector119;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector120;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector121;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector122;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector123;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector124;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector125;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector126;  // dpy, re, event
    public delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, int> EventVector127;  // dpy, re, event
}

