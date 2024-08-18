using ImageQT.SourceGenerator.SourceMaker;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ImageQT.SourceGenerator
{
    [Generator]
    public class DynamicValueGetterGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            Debugger.Break();
            var c = context.SyntaxProvider.ForAttributeWithMetadataName("ImageQT.SourceGenerator.Abstract.DynamicValueGetterAttribute",
                (node, _) => node is StructDeclarationSyntax { AttributeLists.Count: > 0 },
                (ctx, _) =>
                {
                    var result = new List<SyntaxNode>(ctx.Attributes.Length);
                    for (var i = 0; i < ctx.Attributes.Length; i++)
                    {
                        var attr = ctx.Attributes[i];
                        var parameter = attr.NamedArguments.FirstOrDefault(a => a.Key == "ShouldThrow").Value.Value as bool?;
                        if (parameter != null)
                        {
                            continue;
                        }
                        result.Add(attr.ApplicationSyntaxReference!.GetSyntax());

                    }
                    return result;
                }).Collect();


            var targetItems = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (node, _) =>
                    {
                        return node is StructDeclarationSyntax { AttributeLists.Count: > 0 };
                    },
                    (ctx, _) =>
                    {
                        var structDecleration = ctx.Node as StructDeclarationSyntax;
                        if (structDecleration is null)
                            return null;
                        var structSynbols = ctx.SemanticModel.GetDeclaredSymbol(structDecleration) as INamedTypeSymbol;
                        if (structSynbols is null)
                            return null;

                        if (structSynbols.GetAttributes().Any(ad => ad.AttributeClass?.ToDisplayString() == "ImageQT.SourceGenerator.Abstract.DynamicValueGetterAttribute"))
                            return (INamedTypeSymbol?)structSynbols;
                        else
                            return null;
                    })
                .Where(a => a is not null);
            targetItems.Collect();
            //context.RegisterSourceOutput(targetItems, (sourceContext, source) =>
            //{
            //    sourceContext.AddSource($"{source.Name}.g.cs", DynamicValueSource.GenerateText(source, false));
            //});
        }
    }
}
