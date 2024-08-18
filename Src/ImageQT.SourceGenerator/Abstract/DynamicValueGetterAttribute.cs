using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQT.SourceGenerator.Abstract;

[AttributeUsage(AttributeTargets.Struct)]
public class DynamicValueGetterAttribute : Attribute
{
    public bool ShouldThrow { get; }
    public DynamicValueGetterAttribute(bool shouldThrow = false) =>
        ShouldThrow = shouldThrow;
}
