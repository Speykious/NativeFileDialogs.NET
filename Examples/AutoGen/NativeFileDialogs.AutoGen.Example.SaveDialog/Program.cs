// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.Init();

    FilterItem[] filterItem = new[]
    {
        new FilterItem
        {
            Name = "Source code",
            Spec = "c,cpp,cc",
        },
        new FilterItem
        {
            Name = "Headers",
            Spec = "h,hpp",
        },
    };

    sbyte* savePathPtr;
    Result result = nfd.SaveDialog(&savePathPtr, filterItem, (uint)filterItem.Length, null, "Untitled.c");
    switch (result)
    {
        case Result.Okay:
            Console.WriteLine("Success!");
            Console.WriteLine(Marshal.PtrToStringUTF8((nint)savePathPtr));
            break;
        case Result.Cancel:
            Console.WriteLine("User pressed Cancel.");
            break;
        default:
            Console.WriteLine($"Error: {nfd.GetError()}");
            break;
    }

    nfd.Quit();
}
