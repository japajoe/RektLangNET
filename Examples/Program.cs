using System;

namespace VoltLangNET
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "helloworld.vlt";

            Assembly assembly = new Assembly();
            VirtualMachine machine = new VirtualMachine();
            Compiler compiler = new Compiler();

            ModuleLoader.Load(new MathModule());
            ModuleLoader.Load(new SystemModule());            

            if(compiler.CompileFromFile(filepath, assembly))
            {
                if(machine.LoadAssembly(assembly))
                {
                    ExecutionStatus status = ExecutionStatus.Ok;

                    var tp1 = DateTime.Now;                   

                    while(status == ExecutionStatus.Ok)
                    {
                        status = machine.Run();
                    }
                    
                    if(status != ExecutionStatus.Done)
                        Console.WriteLine(status);

                    var tp2 = DateTime.Now;

                    double elapsed = Math.Round((tp2 - tp1).TotalMicroseconds * 0.001, 3);

                    Console.WriteLine("Execution finished in " + elapsed + " milliseconds");
                }
            }

            compiler.Dispose();
            machine.Dispose();
            assembly.Dispose();

            ModuleLoader.Dispose();
        }
    }
}
