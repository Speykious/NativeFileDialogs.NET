// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

public static class Nfd
{
    private static readonly NfdManager manager = new NfdManager();

    public static NfdStatus OpenDialog(out string? outPath, IDictionary<string, string>? filters = null, string? defaultPath = null)
    {
        manager.PushDialog();
        FilterItem[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* outPathPtr;
            status = nfd.OpenDialog(&outPathPtr, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : ToString(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus OpenDialogMultiple(out string[]? outPaths, IDictionary<string, string>? filters = null, string? defaultPath = null)
    {
        manager.PushDialog();
        FilterItem[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            IntPtr pathSet;
            status = nfd.OpenDialogMultiple(&pathSet, filterItems, (uint)filterItems.Length, defaultPath).ToNfdStatus();

            if (status == NfdStatus.Ok)
            {
                uint count = 0;
                nfd.PathSetGetCount(pathSet, ref count).ToNfdStatus();
                outPaths = new string[count];

                for (uint i = 0; i < count; i++)
                {
                    sbyte* pathPtr;
                    nfd.PathSetGetPath(pathSet, i, &pathPtr);
                    outPaths[i] = ToString(pathPtr) ?? "";
                }

                nfd.PathSetFree(pathSet);
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
            status = nfd.PickFolder(&outPathPtr, defaultPath).ToNfdStatus();
            outPath = status == NfdStatus.Cancelled ? null : ToString(outPathPtr);
        }

        manager.PullDialog();
        return status;
    }

    public static NfdStatus SaveDialog(out string? savePath, IDictionary<string, string>? filters = null, string defaultName = "Untitled", string? defaultPath = null)
    {
        manager.PushDialog();
        FilterItem[] filterItems = ToFilterItems(filters);
        NfdStatus status;

        unsafe
        {
            sbyte* savePathPtr;
            status = nfd.SaveDialog(&savePathPtr, filterItems, (uint)filterItems.Length, defaultPath, defaultName).ToNfdStatus();
            savePath = status == NfdStatus.Cancelled ? null : ToString(savePathPtr);
        }

        manager.PullDialog();
        return status;
    }

    internal static FilterItem[] ToFilterItems(IDictionary<string, string>? filters)
    {
        if (filters == null)
            return Array.Empty<FilterItem>();

        FilterItem[] filterItems = new FilterItem[filters.Count];
        int i = 0;
        foreach ((string key, string value) in filters)
        {
            filterItems[i++] = new FilterItem
            {
                Name = key,
                Spec = value,
            };
        }

        return filterItems;
    }

    private static unsafe string? ToString(sbyte* utf8)
    {
        return Marshal.PtrToStringUTF8((nint)utf8);
    }
}
