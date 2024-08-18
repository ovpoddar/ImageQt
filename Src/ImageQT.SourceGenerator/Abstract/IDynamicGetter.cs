using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQT.SourceGenerator.Abstract;
public interface IDynamicGetter
{
    T GetProprityValue<T>(string name);
}
