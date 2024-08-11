// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.Init();

    FilterItem[] filterItems = new[]
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

    IntPtr pathSet;
    Result result = nfd.OpenDialogMultiple(&pathSet, filterItems, (uint)filterItems.Length, null);
    switch (result)
    {
        case Result.Okay:
            Console.WriteLine("Success!");
            uint count = 0;
            nfd.PathSetGetCount(pathSet, ref count);
            string?[] outPaths = new string?[count];

            for (uint i = 0; i < count; i++)
            {
                sbyte* pathPtr;
                nfd.PathSetGetPath(pathSet, i, &pathPtr);
                outPaths[i] = Marshal.PtrToStringUTF8((nint)pathPtr);
            }

            nfd.PathSetFree(pathSet);
            foreach (string? path in outPaths)
                Console.WriteLine($"- {path}");

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
