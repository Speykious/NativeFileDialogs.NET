// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

internal class NfdManager
{
    private uint counter = 0;

    public NfdStatus PushDialog()
    {
        lock (this)
        {
            NfdStatus status = NfdStatus.Ok;
            if (counter == 0)
                status = nfd.Init().ToNfdStatus();

            counter++;
            return status;
        }
    }

    public NfdStatus PullDialog()
    {
        lock (this)
        {
            if (counter == 0)
                return NfdStatus.Cancelled;

            counter--;
            if (counter == 0)
                nfd.Quit();

            return NfdStatus.Ok;
        }
    }
}
