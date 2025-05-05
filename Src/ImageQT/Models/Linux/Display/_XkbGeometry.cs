using System.Runtime.InteropServices;

namespace ImageQT.Models.Linux.Display;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct _XkbGeometry
{
    public ulong Name;
    public ushort WidthMm;
    public ushort HeightMm;
    public char* LabelFont;
    public XkbColorRec* LabelColor;
    public XkbColorRec* BaseColor;
    public ushort SzProperties;
    public ushort SzColors;
    public ushort SzShapes;
    public ushort SzSections;
    public ushort SzDoodads;
    public ushort SzKeyAliases;
    public ushort NumProperties;
    public ushort NumColors;
    public ushort NumShapes;
    public ushort NumSections;
    public ushort NumDoodads;
    public ushort NumKeyAliases;
    public XkbPropertyRec* Properties;
    public XkbColorRec* Colors;
    public XkbShapeRec* Shapes;
    public XkbSectionRec* Sections;
    public nint Doodads; // todo: find a way to implement this XkbDoodadRec
    public XkbKeyAliasRec* KeyAliases;
}