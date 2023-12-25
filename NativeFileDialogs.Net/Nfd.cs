// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Collections.Generic;
using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

public static class Nfd
{
    private static readonly NfdManager manager = new NfdManager();

    public static NfdStatus OpenDialog(out string? outPath, IDictionary<string, string>? filters = null, string? defaultPath = null)
    {
        manager.PushDialog();
        Nfdu8filteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* outPathPtr;
            status = nfd.NFD_OpenDialogU8(&outPathPtr, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : new string(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus OpenDialogMultiple(out string[]? outPaths, IDictionary<string, string>? filters = null, string? defaultPath = null)
    {
        manager.PushDialog();
        Nfdu8filteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            IntPtr pathSet;
            status = nfd.NFD_OpenDialogMultipleU8(&pathSet, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();

            if (status == NfdStatus.Ok)
            {
                uint count = 0;
                nfd.NFD_PathSetGetCount(pathSet, ref count).ToNfdStatus();
                outPaths = new string[count];

                for (uint i = 0; i < count; i++)
                {
                    sbyte* pathPtr;
                    nfd.NFD_PathSetGetPathU8(pathSet, i, &pathPtr);
                    outPaths[i] = new string(pathPtr);
                }

                nfd.NFD_PathSetFree(pathSet);
            }
            else
            {
                outPaths = null;
            }
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus PickFolder(out string? outPath, string? defaultPath = null)
    {
        manager.PushDialog();
        NfdStatus status;

        unsafe
        {
            sbyte* outPathPtr;
            status = nfd.NFD_PickFolderU8(&outPathPtr, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : new string(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus SaveDialog(out string? savePath, IDictionary<string, string>? filters = null, string defaultName = "Untitled", string? defaultPath = null)
    {
        manager.PushDialog();
        Nfdu8filteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* savePathPtr;
            status = nfd.NFD_SaveDialogU8(&savePathPtr, filterItems, (uint)filterItems.Length, defaultPath, defaultName).ToNfdStatus();
            savePath = status == NfdStatus.Cancelled ? null : new string(savePathPtr);
        }

        manager.PullDialog();
        return status;
    }

    internal static Nfdu8filteritemT[] ToFilterItems(IDictionary<string, string>? filters)
    {
        if (filters == null)
            return Array.Empty<Nfdu8filteritemT>();

        Nfdu8filteritemT[] filterItems = new Nfdu8filteritemT[filters.Count];
        int i = 0;
        foreach ((string key, string value) in filters)
        {
            filterItems[i++] = new Nfdu8filteritemT
            {
                Name = key,
                Spec = value,
            };
        }

        return filterItems;
    }
}
