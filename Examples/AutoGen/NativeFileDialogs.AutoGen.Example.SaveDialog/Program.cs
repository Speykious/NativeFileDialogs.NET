// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.Init();

    FilterItemU8[] filterItem = new[]
    {
        new FilterItemU8
        {
            Name = "Source code",
            Spec = "c,cpp,cc",
        },
        new FilterItemU8
        {
            Name = "Headers",
            Spec = "h,hpp",
        },
    };

    sbyte* savePathPtr;
    Result result = nfd.SaveDialogU8(&savePathPtr, filterItem, (uint)filterItem.Length, null, "Untitled.c");
    switch (result)
    {
        case Result.Okay:
            Console.WriteLine("Success!");
            Console.WriteLine(new string(savePathPtr));
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
