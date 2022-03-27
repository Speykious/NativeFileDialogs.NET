// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.NFD_Init();

    NfdnfilteritemT[] filterItem = new[]
    {
        new NfdnfilteritemT
        {
            Name = "Source code",
            Spec = "c,cpp,cc",
        },
        new NfdnfilteritemT
        {
            Name = "Headers",
            Spec = "h,hpp",
        },
    };

    sbyte* savePathPtr;
    NfdresultT result = nfd.NFD_SaveDialogN(&savePathPtr, filterItem, (uint)filterItem.Length, null, "Untitled.c");
    switch (result)
    {
        case NfdresultT.NFD_OKAY:
            Console.WriteLine("Success!");
            Console.WriteLine(new string(savePathPtr));
            break;
        case NfdresultT.NFD_CANCEL:
            Console.WriteLine("User pressed Cancel.");
            break;
        default:
            Console.WriteLine($"Error: {nfd.NFD_GetError()}");
            break;
    }

    nfd.NFD_Quit();
}
