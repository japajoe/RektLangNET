using System;

namespace VoltLangNET
{
    public unsafe class SystemModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction print;
        private static VoltVMFunction println;
        private static VoltVMFunction printhx;
        private static VoltVMFunction endln;
        private static VoltVMFunction timestamp;
        private static VoltVMFunction get_module_handle;

        public SystemModule()
        {
            print = Print;
            println = PrintLine;
            printhx = PrintHex;
            endln = EndLine;
            timestamp = TimeStamp;
            get_module_handle = GetModuleHandle;
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("print", print);
            VirtualMachine.RegisterFunction("println", println);
            VirtualMachine.RegisterFunction("printhx", printhx);
            VirtualMachine.RegisterFunction("endln", endln);
            VirtualMachine.RegisterFunction("timestamp", timestamp);
            VirtualMachine.RegisterFunction("get_module_handle", get_module_handle);
        }

        public void Dispose()
        {

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

        private static int PrintHex(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (!stack.TryPopAsUInt64(buffer, out ulong value, out ulong offset))
            {
                return -1;
            }

            Console.Write("0x{0:x}", value);

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
