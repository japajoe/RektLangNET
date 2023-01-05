# VoltLangNET
A stack and register based virtual machine which can compile and execute arbitrary code in runtime.

Support for integer types like
- int64/uint64
- double

# About
This is a .NET wrapper for https://github.com/japajoe/volt

# Note
- Instruction opcodes are case sensitive and MUST be lower case!
- Get the native libraries (x64) for Windows and Linux from the [libs](https://github.com/japajoe/VoltLangNET/tree/main/libs) folder.
- You need .net 7 for this library. You might be able to try it on 6 or even 5 but I haven't tested that.

# Setting up application
```csharp
using System;
using VoltLangNET;

namespace VoltLangNETApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "fibonacci.vlt";

            Assembly assembly = new Assembly();
            VirtualMachine machine = new VirtualMachine();
            Compiler compiler = new Compiler();

            ModuleLoader.Load(new MathModule());
            ModuleLoader.Load(new SystemModule());
            ModuleLoader.Load(new MemoryModule());

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

            ModuleLoader.Dispose();
        }
    }
}
```

# Examples
Check [here](https://github.com/japajoe/VoltLangNET/tree/main/Examples).

# Instructions
**MOV**
Move a value into a register or into a variable. Registers are able to store any type, variables are constrained to only contain the type they are defined as.
- mov eax, ebx
- mov eax, myVariable
- mov eax, 10
- mov myVariable, eax
- mov myVariable, anotherVariable
- mov myVariable, 123.45

**INC**
Increments the value stored in a register or variable with 1.
- inc eax
- inc myVariable

**DEC**
Decrements the value stored in a register or variable with 1.
- dec eax
- dec myVariable

**ADD**
Adds a value to a value stored in a register or variable.
- add eax, 30
- add eax, myVariable
- add myVariable, 30
- add myVariable, eax

**SUB**
Subtract a value from a value stored in a register or variable.
- sub eax, 30
- sub eax, myVariable
- sub myVariable, 30
- sub myVariable, eax

**MUL**
Multiplies a value stored in a register or variable with another value.
- mul eax, 30
- mul eax, myVariable
- mul myVariable, 30
- mul myVariable, eax

**DIV**
Divides a value stored in a register or variable with another value.
- div eax, 30
- div eax, myVariable
- div myVariable, 30
- div myVariable, eax

**PUSH**
Pushes a value to the stack.
- push eax
- push myVariable
- push 10
- push 0.5

**POP**
Pops any value off the top of the stack.
- pop
- pop eax
- pop myVariable

**CMP**
Compares 2 values for equality and sets a flag whether the lefthandside is less than, greater than, or equal to the righthandside.
- cmp eax, ebx
- cmp eax, myVariable
- cmp eax, 99
- cmp myVariable, ebx
- cmp myVariable, anotherVariable
- cmp myVariable, 45

**CALL**
Makes a jump to a specific label defined by the user, OR calls predefined functions provided by your application. A label can be defined above any instruction and it could be seen like a function identifier. If a jump to a label is made, the return address will get placed on top of the stack. Then whenever a RET instruction is being executed, the return address will get popped off the top of the stack and the instruction pointer is set to this address. In order to prevent corruption of the stack, it is very important that you take good care of what you push and pop to/from the stack.
- call doWork
- call printInfo
- call doCalculation

**RET**
The RET instruction sets the instructionpointer back to the instruction that comes right after the CALL instruction. See description about the CALL opcode to get an understanding of how these two instructions relate.

**JMP**
Unconditional jump to a label
- jmp someLabel

**JE**
Jump to label if CMP instruction evaluated to equal.
- je someLabel

**JNE**
Jump to label if CMP instruction evaluated to not equal.
- jne someLabel

**JG**
Jump to label if CMP instruction evaluated to greater than.
- jg someLabel

**JGE**
Jump to label if CMP instruction evaluated to greater than or equal.
- jge someLabel

**JL**
Jump to label if CMP instruction evaluated to less than.
- jl someLabel

**JLE**
Jump to label if CMP instruction evaluated to less than or equal.
- jle someLabel

**JZ**
Jump to label if result of a add/sub/mul/div instruction equals to zero.
- jz someLabel

**JNZ**
Jump to label if result of a add/sub/mul/div instruction does not equal zero.
- jz someLabel

**NOP**
This opcode literally does nothing. In x86 assembly it can be used to disable instructions by overwriting them with NOP instructions

**HLT**
This effectively stops the program.
