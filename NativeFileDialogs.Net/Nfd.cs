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
        NfdnfilteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* outPathPtr;
            status = nfd.NFD_OpenDialogN(&outPathPtr, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : new string(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus OpenDialogMultiple(out string[]? outPaths, IDictionary<string, string>? filters = null, string? defaultPath = null)
    {
        manager.PushDialog();
        NfdnfilteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            IntPtr pathSet;
            status = nfd.NFD_OpenDialogMultipleN(&pathSet, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();

            if (status == NfdStatus.Ok)
            {
                uint count = 0;
                nfd.NFD_PathSetGetCount(pathSet, ref count).ToNfdStatus();
                outPaths = new string[count];

                for (uint i = 0; i < count; i++)
                {
                    sbyte* pathPtr;
                    nfd.NFD_PathSetGetPathN(pathSet, i, &pathPtr);
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
            status = nfd.NFD_PickFolderN(&outPathPtr, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : new string(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus SaveDialog(out string? savePath, IDictionary<string, string>? filters = null, string defaultName = "Untitled", string? defaultPath = null)
    {
        manager.PushDialog();
        NfdnfilteritemT[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* savePathPtr;
            status = nfd.NFD_SaveDialogN(&savePathPtr, filterItems, (uint)filterItems.Length, defaultPath, defaultName).ToNfdStatus();
            savePath = status == NfdStatus.Cancelled ? null : new string(savePathPtr);
        }

        manager.PullDialog();
        return status;
    }

    internal static NfdnfilteritemT[] ToFilterItems(IDictionary<string, string>? filters)
    {
        if (filters == null)
            return Array.Empty<NfdnfilteritemT>();

        NfdnfilteritemT[] filterItems = new NfdnfilteritemT[filters.Count];
        int i = 0;
        foreach ((string key, string value) in filters)
        {
            filterItems[i++] = new NfdnfilteritemT
            {
                Name = key,
                Spec = value,
            };
        }

        return filterItems;
    }
}
