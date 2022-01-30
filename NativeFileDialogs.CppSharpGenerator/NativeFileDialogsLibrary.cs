// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace NativeFileDialogs.CppSharpGenerator;

public class NativeFileDialogsLibrary : ILibrary
{
    private const string lib_name = "NativeFileDialogs";

    public void Setup(Driver driver)
    {
        DriverOptions options = driver.Options;
        options.GeneratorKind = GeneratorKind.CSharp;
        options.OutputDir = $"../{lib_name}.AutoGen";
        var module = options.AddModule("nfd");
        module.IncludeDirs.Add(@"../nativefiledialog-extended/src/include");
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
    }
}
