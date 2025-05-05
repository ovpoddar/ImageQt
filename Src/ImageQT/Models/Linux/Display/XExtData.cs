namespace ImageQT.Models.Linux.Display;

public unsafe struct XExtData
{
    public int Number;
    public XExtData* Next; /* next item on list of data for structure */
    public delegate* unmanaged[Cdecl]<XExtData*, int> FreePrivate; /* function to free private data */
    public nint PrivateData; /* data private to this extension. */
}