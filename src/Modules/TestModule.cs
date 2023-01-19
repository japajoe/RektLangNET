using System;

namespace RektLangNET
{
    public unsafe class TestModule : IModule
    {
        private static RektVMFunction testFunction;

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

            RektObject obj;

            if(stack.Pop(out obj))
            {
                Console.WriteLine(obj.type);
                Console.WriteLine(obj.as_double);
            }

            return 0;
        }
    }
}
