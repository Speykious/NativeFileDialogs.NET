// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.Net;

NfdStatus result = Nfd.PickFolder(out string? outPath);

if (result == NfdStatus.Ok)
{
    Console.WriteLine("Success!");
    Console.WriteLine(outPath);
}
else
{
    Console.WriteLine("User pressed Cancel.");
}
