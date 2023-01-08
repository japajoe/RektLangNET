using System;

namespace VoltLangNET
{
    [Obsolete("This module is implemented in the native library and only serves as an example of how to make your own module.")]
    public unsafe class SystemModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction printf;

        public SystemModule()
        {
            printf = PrintF;
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("printf", printf);
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
    }
}
