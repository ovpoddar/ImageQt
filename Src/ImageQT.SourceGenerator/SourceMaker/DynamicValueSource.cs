using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ImageQT.SourceGenerator.SourceMaker;
internal static class DynamicValueSource
{
    public static SourceText GenerateText(INamedTypeSymbol source, bool shouldThrow)
    {
        var structProprities = source
            .GetMembers()
            .Where(a => a.DeclaredAccessibility == Accessibility.Public
                && a.Kind == SymbolKind.Property);
        var stringBuilder = new StringBuilder("using System; \n");
        stringBuilder.AppendLine(source.ContainingNamespace.IsGlobalNamespace ? "" : $"namespace {source.ContainingNamespace.ToString()};");
        stringBuilder.AppendLine($"internal partial struct {source.Name}() : global::ImageQT.SourceGenerator.Abstract.IDynamicGetter \n{{");
        stringBuilder.AppendLine($"\t public T  GetProprityValue<T>(string name) =>");
        stringBuilder.AppendLine($"\t name switch \n{{");
        foreach (var structMember in structProprities)
        {
            stringBuilder.AppendLine($"\"{structMember.Name}\" => (T)(object)this.{structMember.Name},");
        }
        if (shouldThrow)
            stringBuilder.AppendLine("_ => throw new Exception()");
        else
            stringBuilder.AppendLine("_ => (T)default");
        stringBuilder.AppendLine("}; \n }");

        return SourceText.From(stringBuilder.ToString(), Encoding.UTF8);
    }
}
