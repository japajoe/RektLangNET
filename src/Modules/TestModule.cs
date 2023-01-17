using System;

namespace VoltLangNET
{
    public unsafe class TestModule : IModule
    {
        private static VoltVMFunction testFunction;

        public TestModule()
        {
            testFunction = TestFunction;
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("testfunction", testFunction);
        }

        public void Dispose()
        {

        }

        private static int TestFunction(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            VoltObject obj;

            if(stack.Pop(out obj))
            {
                Console.WriteLine(obj.type);

                Console.WriteLine(obj.as_double);
            }

            return 0;
        }
    }
}
