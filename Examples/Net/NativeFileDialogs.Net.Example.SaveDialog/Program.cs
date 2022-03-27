// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Collections.Generic;
using NativeFileDialogs.Net;

NfdStatus result = Nfd.SaveDialog(out string? savePath, new Dictionary<string, string>()
{
    { "Source code", "c,cpp,cc" },
    { "Headers", "h,hpp" },
}, defaultName: "Untitled.c");

if (result == NfdStatus.Ok)
{
    Console.WriteLine("Success!");
    Console.WriteLine(savePath);
}
else
{
    Console.WriteLine("User pressed Cancel.");
}
