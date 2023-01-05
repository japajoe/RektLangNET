using System;

namespace VoltLangNET
{
    public unsafe class SystemModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction printf;
        private static VoltVMFunction timestamp;
        private static VoltVMFunction get_module_handle;

        public SystemModule()
        {
            printf = PrintF;
            timestamp = TimeStamp;
            get_module_handle = GetModuleHandle;
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("printf", printf);
            VirtualMachine.RegisterFunction("timestamp", timestamp);
            VirtualMachine.RegisterFunction("get_module_handle", get_module_handle);
        }

        public void Dispose()
        {

        }

        private static int PrintF(StackPointer sp)
        {
            Stack stack = new Stack(sp);
            
            ulong offset = 0;
            ulong stackCount = stack.GetCount();

            if(stackCount == 0)
            {
                Console.WriteLine();
            }
            else if(stackCount == 1)
            {
                if (!stack.TryPopAsString(buffer, out string message, out offset))
                    return -1;

                Console.Write(message);
            }
            else
            {
                if (!stack.TryPopAsString(buffer, out string message, out offset))
                    return -1;

                object[] args = new object[stackCount - 1];
                for (ulong i = 0; i < stackCount-1; i++)
                {
                    if (!stack.TryPopAsObject(buffer, out object value, out offset))
                        return -1;

                    args[i] = value;
                }

                Console.Write(message, args);
                
            }

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
