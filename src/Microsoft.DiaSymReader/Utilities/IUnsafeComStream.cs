﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace Microsoft.DiaSymReader
{
    // TODO: Copied from Roslyn. Share.

    /// <summary>
    /// This is a re-definition of COM's IStream interface. The important change is that
    /// the Read and Write methods take an <see cref="IntPtr"/> instead of a byte[] to avoid the
    /// allocation cost when called from native code.
    /// </summary>
    [Guid("0000000c-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComImport]
    internal unsafe interface IUnsafeComStream
    {
        // ISequentialStream portion
        void Read(byte* pv, int cb, int* pcbRead);
        void Write(byte* pv, int cb, int* pcbWritten);

        // IStream portion
        void Seek(long dlibMove, int dwOrigin, long* plibNewPosition);
        void SetSize(long libNewSize);
        void CopyTo(IStream pstm, long cb, int* pcbRead, int* pcbWritten);
        void Commit(int grfCommitFlags);
        void Revert();
        void LockRegion(long libOffset, long cb, int dwLockType);
        void UnlockRegion(long libOffset, long cb, int dwLockType);
        void Stat(out STATSTG pstatstg, int grfStatFlag);
        void Clone(out IStream ppstm);
    }
}
