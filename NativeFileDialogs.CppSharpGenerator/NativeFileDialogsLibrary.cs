// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
        driver.Context.TranslationUnitPasses.RemovePrefix("NFD_");
        driver.Context.TranslationUnitPasses.RemovePrefix("nfd");

        driver.Context.TranslationUnitPasses.RenameWithPattern("^(.+)(N|(U8))_(With)_?(Impl)?$", "$1$4$2$5", RenameTargets.Function);

        driver.Context.TranslationUnitPasses.RenameDeclsUpperCase(RenameTargets.Any);

        driver.Context.TranslationUnitPasses.AddPass(new RegexRenamePass("^([A-Z]+)$", m => m.Value.ToLowerInvariant(), RenameTargets.EnumItem));

        driver.Context.TranslationUnitPasses.RenameWithPattern("T$", "", RenameTargets.Enum | RenameTargets.Class);

        driver.Context.TranslationUnitPasses.RenameWithPattern("^(.+)filteritem$", "FilterItem$1", RenameTargets.Enum | RenameTargets.Class);

        driver.Context.TranslationUnitPasses.RenameWithPattern("(Pathsetenum)|(PathSetEnum)", "PathSetEnumerator", RenameTargets.Any);

        driver.Context.TranslationUnitPasses.RenameWithPattern("^(.+)filteritem$", "FilterItem$1", RenameTargets.Class);

        driver.Context.TranslationUnitPasses.RenameWithPattern("^Windowhandle$", "WindowHandle", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Pickfoldernargs$", "PickFolderArgsN", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Pickfolderu8args$", "PickFolderArgsU8", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Opendialognargs$", "OpenDialogArgsN", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Opendialogu8args$", "OpenDialogArgsU8", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Savedialognargs$", "SaveDialogArgsN", RenameTargets.Class);
        driver.Context.TranslationUnitPasses.RenameWithPattern("^Savedialogu8args$", "SaveDialogArgsU8", RenameTargets.Class);

        driver.Context.TranslationUnitPasses.RenameWithPattern("U8$", "", RenameTargets.Any);
    }

    public void Preprocess(Driver driver, ASTContext ctx)
    {
    }

    public void Postprocess(Driver driver, ASTContext ctx)
    {
        foreach (var function in ctx.TranslationUnits.SelectMany(t => t.Functions).Where(f => f.Name.EndsWith('N') || f.Name.EndsWith("Impl")))
        {
            function.Ignore = true;
        }

        foreach (var @class in ctx.TranslationUnits.SelectMany(t => t.Classes).Where(c => c.Name.EndsWith('N')))
        {
            @class.Ignore = true;
        }
    }

    public void GenerateCode(Driver driver, List<GeneratorOutput> outputs)
    {
    }

    private class RegexRenamePass : RenamePass
    {
        public string Pattern { get; }

        public MatchEvaluator MatchEvaluator { get; }

        public RegexRenamePass([StringSyntax("regex")] string pattern, MatchEvaluator matchEvaluator, RenameTargets targets = RenameTargets.Any)
        {
            Pattern = pattern;
            MatchEvaluator = matchEvaluator;
            Targets = targets;
        }

        public override bool Rename(Declaration decl, [NotNullWhen(true)] out string? newName)
        {
            if (base.Rename(decl, out newName))
            {
                return true;
            }

            string text = Regex.Replace(decl.Name, Pattern, MatchEvaluator);
            if (!decl.Name.Equals(text, System.StringComparison.Ordinal))
            {
                newName = text;
                return true;
            }

            newName = null;
            return false;
        }

        public override string ToString() => "RegexRenamePass: " + Pattern;
    }
}
