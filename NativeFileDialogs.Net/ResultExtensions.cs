// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

internal static class ResultExtensions
{
    public static NfdStatus ToNfdStatus(this Result result)
    {
        return result switch
        {
            Result.Okay => NfdStatus.Ok,
            Result.Cancel => NfdStatus.Cancelled,
            _ => throw new NfdException(),
        };
    }
}
