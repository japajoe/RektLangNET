using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StackPointer
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