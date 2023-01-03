using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StackPointer
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