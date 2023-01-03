using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    public unsafe class SystemModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction print;
        private static VoltVMFunction println;
        private static VoltVMFunction endln;
        private static VoltVMFunction timestamp;
        private static VoltVMFunction pushstring;
        private static VoltVMFunction get_module_handle;

        private static string testString = "This is some test string\n";
        private static IntPtr testStringPtr;

        public SystemModule()
        {
            print = Print;
            println = PrintLine;
            endln = EndLine;
            timestamp = TimeStamp;
            pushstring = PushString;
            get_module_handle = GetModuleHandle;

            testStringPtr = Marshal.StringToHGlobalAnsi(testString);
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("print", print);
            VirtualMachine.RegisterFunction("println", println);
            VirtualMachine.RegisterFunction("endln", endln);
            VirtualMachine.RegisterFunction("timestamp", timestamp);
            VirtualMachine.RegisterFunction("push_string", pushstring);
            VirtualMachine.RegisterFunction("get_module_handle", get_module_handle);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(testStringPtr);
        }

        private static int PushString(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            Console.WriteLine("C# PushString address {0:x}", testStringPtr);

            if(!stack.PushString(testStringPtr, out ulong offset))
            {
                return -1;
            }

            return 0;
        }

        private static int Print(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (!stack.TryPopAsString(buffer, out string value, out ulong offset))
            {
                return -1;
            }

            Console.Write(value);

            return 0;
        }

        private static int PrintLine(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (!stack.TryPopAsString(buffer, out string value, out ulong offset))
            {
                return -1;
            }

            Console.WriteLine(value);

            return 0;
        }

        private static int EndLine(StackPointer sp)
        {
            Console.WriteLine();
            return 0;
        }

        private static int TimeStamp(StackPointer sp)        
        {
            Stack stack = new Stack(sp);
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            stack.PushInt64(timestamp, out ulong offset);
            return 0;
        }

        private static int GetModuleHandle(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (!stack.TryPopAsString(buffer, out string value, out ulong offset))
            {
                return -1;
            }

            VoltNative.volt_get_module_address(value, out ulong address);

            stack.PushUInt64(address, out offset);

            return 0;            
        }
    }
}