// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

namespace NativeFileDialogs.Net;

/// <summary>
/// Status of the file dialog after it gets closed.
/// </summary>
public enum NfdStatus
{
    /// <summary>
    /// Action was performed successfully.
    /// </summary>
    Ok,
    /// <summary>
    /// User pressed the cancel button.
    /// </summary>
    Cancelled,
}
