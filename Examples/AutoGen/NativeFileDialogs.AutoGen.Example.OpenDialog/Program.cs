﻿// Copyright (c) Speykious
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

    sbyte* outPathPtr;
    NfdresultT result = nfd.NFD_OpenDialogN(&outPathPtr, filterItem, (uint)filterItem.Length, null);
    switch (result)
    {
        case NfdresultT.NFD_OKAY:
            Console.WriteLine("Success!");
            Console.WriteLine(new string(outPathPtr));
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