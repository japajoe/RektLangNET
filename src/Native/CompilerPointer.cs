using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CompilerPointer
    {
        public IntPtr pointer;

        public bool IsValid
        {
            get
            {
                return pointer != IntPtr.Zero;
            }
        }
    }
}