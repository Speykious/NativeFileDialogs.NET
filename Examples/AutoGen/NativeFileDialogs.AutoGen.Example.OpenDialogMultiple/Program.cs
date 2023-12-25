// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.NFD_Init();

    Nfdu8filteritemT[] filterItems = new[]
    {
        new Nfdu8filteritemT
        {
            Name = "Source code",
            Spec = "c,cpp,cc",
        },
        new Nfdu8filteritemT
        {
            Name = "Headers",
            Spec = "h,hpp",
        },
    };

    IntPtr pathSet;
    NfdresultT result = nfd.NFD_OpenDialogMultipleU8(&pathSet, filterItems, (uint)filterItems.Length, null);
    switch (result)
    {
        case NfdresultT.NFD_OKAY:
            Console.WriteLine("Success!");
            uint count = 0;
            nfd.NFD_PathSetGetCount(pathSet, ref count);
            string[] outPaths = new string[count];

            for (uint i = 0; i < count; i++)
            {
                sbyte* pathPtr;
                nfd.NFD_PathSetGetPathU8(pathSet, i, &pathPtr);
                outPaths[i] = new string(pathPtr);
            }

            nfd.NFD_PathSetFree(pathSet);
            foreach (string path in outPaths)
                Console.WriteLine($"- {path}");

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
