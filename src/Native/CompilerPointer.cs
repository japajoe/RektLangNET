using System;
using System.Runtime.InteropServices;

namespace RektLangNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CompilerPointer
    {
        public UIntPtr pointer;

        public bool IsValid
        {
            get
            {
                return pointer != UIntPtr.Zero;
            }
        }
    }
}