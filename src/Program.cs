using System;
using VoltLangNET;

namespace VoltLangNETApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "fibonacci.asm";

            if(args.Length > 0)
                filepath = args[0];

            Assembly assembly = new Assembly();
            VirtualMachine machine = new VirtualMachine();
            Compiler compiler = new Compiler();

            //Loads modules located in the native library
            ModuleLoader.LoadDefaultModules();
            ModuleLoader.Load(new TestModule());

            if(compiler.CompileFromFile(filepath, assembly))
            {
                if (machine.LoadAssembly(assembly))
                {
                    ExecutionStatus status = ExecutionStatus.Ok;

                    var tp1 = DateTime.Now;

                    while (status == ExecutionStatus.Ok)
                    {
                        status = machine.Run();
                    }

                    if (status != ExecutionStatus.Done)
                        Console.WriteLine(status);

                    var tp2 = DateTime.Now;

                    double elapsed = Math.Round((tp2 - tp1).TotalMicroseconds * 0.001, 3);

                    Console.WriteLine("Execution finished in " + elapsed + " milliseconds");
                }
            }

            compiler.Dispose();
            machine.Dispose();
            assembly.Dispose();

            //Releases resources allocated by modules (if any).
            ModuleLoader.Dispose();
        }
    }
}