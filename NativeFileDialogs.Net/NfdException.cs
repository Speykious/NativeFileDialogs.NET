// Copyright (c) Speykious
// This file is part of NativeFileDialogs.NET.
// NativeFileDialogs.NET is licensed under the Zlib License. See LICENSE for details.

using System;
using NativeFileDialogs.AutoGen;

namespace NativeFileDialogs.Net;

public class NfdException : Exception
{
    public NfdException() : base(nfd.NFD_GetError()) { }
}
