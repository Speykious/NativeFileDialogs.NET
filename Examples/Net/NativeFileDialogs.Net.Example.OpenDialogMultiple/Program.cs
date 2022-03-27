// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Collections.Generic;
using NativeFileDialogs.Net;

NfdStatus result = Nfd.OpenDialogMultiple(out string[]? outPaths, new Dictionary<string, string>()
{
    { "Source code", "c,cpp,cc" },
    { "Headers", "h,hpp" },
});

if (result == NfdStatus.Ok && outPaths is string[] paths)
{
    Console.WriteLine("Success!");
    foreach (string path in paths)
        Console.WriteLine($"- {path}");
}
else
{
    Console.WriteLine("User pressed Cancel.");
}
