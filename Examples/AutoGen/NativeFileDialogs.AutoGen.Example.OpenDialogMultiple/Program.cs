// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.Init();

    FilterItemU8[] filterItems = new[]
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

    IntPtr pathSet;
    Result result = nfd.OpenDialogMultipleU8(&pathSet, filterItems, (uint)filterItems.Length, null);
    switch (result)
    {
        case Result.Okay:
            Console.WriteLine("Success!");
            uint count = 0;
            nfd.PathSetGetCount(pathSet, ref count);
            string[] outPaths = new string[count];

            for (uint i = 0; i < count; i++)
            {
                sbyte* pathPtr;
                nfd.PathSetGetPathU8(pathSet, i, &pathPtr);
                outPaths[i] = new string(pathPtr);
            }

            nfd.PathSetFree(pathSet);
            foreach (string path in outPaths)
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
