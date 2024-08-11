// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using NativeFileDialogs.AutoGen;

unsafe
{
    nfd.Init();

    sbyte* outPathPtr;
    Result result = nfd.PickFolder(&outPathPtr, null);
    switch (result)
    {
        case Result.Okay:
            Console.WriteLine("Success!");
            Console.WriteLine(Marshal.PtrToStringUTF8((nint)outPathPtr));
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
