// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

internal static class NfdresultTExtensions
{
    public static NfdStatus ToNfdStatus(this NfdresultT result)
    {
        return result switch
        {
            NfdresultT.NFD_OKAY => NfdStatus.Ok,
            NfdresultT.NFD_CANCEL => NfdStatus.Cancelled,
            _ => throw new NfdException(),
        };
    }
}
