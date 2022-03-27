// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Collections.Generic;
using NativeFileDialogs.Net;

NfdStatus result = Nfd.OpenDialog(out string? outPath, new Dictionary<string, string>()
{
    { "Source code", "c,cpp,cc" },
    { "Headers", "h,hpp" },
});

if (result == NfdStatus.Ok && outPath is string path)
{
    Console.WriteLine("Success!");
    Console.WriteLine(path);
}
else
{
    Console.WriteLine("User pressed Cancel.");
}
