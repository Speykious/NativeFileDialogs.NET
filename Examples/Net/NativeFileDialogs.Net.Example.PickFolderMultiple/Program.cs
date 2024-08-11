// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.Net;

NfdStatus result = Nfd.PickFolderMultiple(out string[]? outPaths);

if (result == NfdStatus.Ok && outPaths is not null)
{
    Console.WriteLine("Success!");
    foreach (string path in outPaths)
        Console.WriteLine($"- {path}");
}
else
{
    Console.WriteLine("User pressed Cancel.");
}
