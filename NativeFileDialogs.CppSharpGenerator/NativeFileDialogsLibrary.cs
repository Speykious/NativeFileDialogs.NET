// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace NativeFileDialogs.CppSharpGenerator;

public class NativeFileDialogsLibrary : ILibrary
{
    private const string lib_name = "NativeFileDialogs";
    private const string include_directory = "./include";

    public void Setup(Driver driver)
    {
        Directory.CreateDirectory(include_directory);

        string fileText = File.ReadAllText(@"../nativefiledialog-extended/src/include/nfd.h")
            .Replace("const nfdnfilteritem_t* filterList,", "const nfdnfilteritem_t filterList[],")
            .Replace("const nfdu8filteritem_t* filterList,", "const nfdu8filteritem_t filterList[],");
        //These replacements are necessary because CppSharp incorrectly generates the former as an object reference.

        File.WriteAllText(include_directory + "/nfd.h", fileText);

        DriverOptions options = driver.Options;
        options.GeneratorKind = GeneratorKind.CSharp;
        options.OutputDir = $"../{lib_name}.AutoGen";
        options.UseSpan = true;
        var module = options.AddModule("nfd");
        module.IncludeDirs.Add(include_directory);
        module.Headers.Add("nfd.h");
        module.OutputNamespace = $"{lib_name}.AutoGen";
    }

    public void SetupPasses(Driver driver)
    {
        driver.Context.TranslationUnitPasses.RenameDeclsUpperCase(RenameTargets.Any);
    }

    public void Preprocess(Driver driver, ASTContext ctx)
    {
    }

    public void Postprocess(Driver driver, ASTContext ctx)
    {
        foreach (var function in ctx.TranslationUnits.SelectMany(t => t.Functions).Where(f => f.Name.EndsWith('N')))
        {
            AddWindowsOnlyAttribute(function);
        }
        AddWindowsOnlyAttribute(ctx.FindClass("NfdnfilteritemT").First());
    }

    public void GenerateCode(Driver driver, List<GeneratorOutput> outputs)
    {
    }

    private static void AddWindowsOnlyAttribute(Declaration declaration)
    {
        declaration.Attributes.Add(new Attribute() { Type = typeof(SupportedOSPlatformAttribute), Value = "\"windows\"" });
    }
}
