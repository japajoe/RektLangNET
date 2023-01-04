using System;

namespace VoltLangNET
{
    public unsafe class MemoryModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction memalloc;
        private static VoltVMFunction memset;
        private static VoltVMFunction memcopy;
        private static VoltVMFunction memfree;
        private static VoltVMFunction memdump;

        public MemoryModule()
        {
            memalloc = MemAlloc;
            memset = MemSet;
            memcopy = MemCopy;
            memfree = MemFree;
            memdump = MemDump;
        }
        
        public void Register()
        {
            VirtualMachine.RegisterFunction("mem_alloc", memalloc);
            VirtualMachine.RegisterFunction("mem_set", memset);
            VirtualMachine.RegisterFunction("mem_copy", memcopy);
            VirtualMachine.RegisterFunction("mem_free", memfree);
            VirtualMachine.RegisterFunction("mem_dump", memdump);
        }

        public void Dispose()
        {
            
        }

        private static int MemAlloc(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (stack.GetSize() == 0)
            {
                Console.WriteLine("MemAlloc stack size should 1 but is 0");
                return -1;
            }

            ulong stackSize = stack.GetSize() / 8;

            if(stackSize != 1)
            {
                Console.WriteLine("MemAlloc stack size should 1 but is " + stackSize);
                return -1;
            }            

            if(!stack.TryPopAsUInt64(buffer, out ulong size, out ulong offset))
                return -1;

            IntPtr pointer = VoltNative.volt_memory_alloc(size);

            stack.PushInt64(pointer.ToInt64(), out offset);

            return 0;
        }

        private static int MemSet(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (stack.GetSize() == 0)
            {
                Console.WriteLine("MemSet stack size should 1 but is 0");
                return -1;
            }

            ulong stackSize = stack.GetSize() / 8;

            if(stackSize != 3)
            {
                Console.WriteLine("MemSet stack size should be 3 but is " + stackSize);
                return -1;
            }

            if (!stack.TryPopAsUInt64(buffer, out ulong size, out ulong offset))
            {
                Console.WriteLine("MemSet fail 1");
                return -1;
            }

            if (!stack.TryPopAsInt64(buffer, out long value, out offset))
            {
                Console.WriteLine("MemSet fail 2");
                return -1;
            }

            if (!stack.TryPopAsInt64(buffer, out long pointer, out offset))
            {
                Console.WriteLine("MemSet fail 3");
                return -1;
            }

            VoltNative.volt_memory_set(new IntPtr(pointer), (int)value, size);

            return 0;
        }

        private static int MemCopy(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (stack.GetSize() == 0)
            {
                Console.WriteLine("MemCopy stack size should 3 but is 0");
                return -1;
            }

            ulong stackSize = stack.GetSize() / 8;

            if(stackSize != 3)
            {
                Console.WriteLine("MemCopy stack size should be 3 but is " + stackSize);
                return -1;
            }            

            if(!stack.TryPopAsUInt64(buffer, out ulong size, out ulong offset))
                return -1;

            if(!stack.TryPopAsInt64(buffer, out long dest, out offset))
                return -1;

            if(!stack.TryPopAsInt64(buffer, out long src, out offset))
                return -1;            

            VoltNative.volt_memory_copy(new IntPtr(src), new IntPtr(dest), size);

            return 0;
        }

        private static int MemFree(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (stack.GetSize() == 0)
            {
                Console.WriteLine("MemFree stack size should 1 but is 0");
                return -1;
            }

            ulong stackSize = stack.GetSize() / 8;

            if(stackSize != 1)
            {
                Console.WriteLine("MemFree stack size should be 1 but is " + stackSize);
                return -1;
            }              

            if(!stack.TryPopAsInt64(buffer, out long src, out ulong offset))
                return -1;

            VoltNative.volt_memory_free(new IntPtr(src));

            return 0;
        }

        private static int MemDump(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if (stack.GetSize() == 0)
            {
                Console.WriteLine("MemDump stack size should 2 but is 0");
                return -1;
            }

            ulong stackSize = stack.GetSize() / 8;

            if(stackSize != 2)
            {
                Console.WriteLine("MemDump stack size should be 2 but is " + stackSize);
                return -1;
            }              

            if(!stack.TryPopAsUInt64(buffer, out ulong size, out ulong offset))
                return -1;            

            if(!stack.TryPopAsInt64(buffer, out long src, out offset))
                return -1;

            UInt64 data = *(UInt64*)src;

            Console.WriteLine("MemDump " + data);

            return 0;
        }        
    }
}