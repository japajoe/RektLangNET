# VoltLangNET
A stack and register based virtual machine which can compile and execute arbitrary code in runtime.

Support for integer types like
- int64/uint64
- double

# About
This is a .NET wrapper for https://github.com/japajoe/volt

# Note
Instruction opcodes are case sensitive and MUST be lower case!

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
Makes a jump to a specific label defined by the user, OR calls predefined functions inside the library. A label can be defined above any instruction and it could be seen like a function identifier. After the CALL instruction the instructionpointer is set to the first instruction after the label. When  instructions have been executed and the RET instruction has been reached, the instructionpointer is set to the instruction after the CALL instruction. This is useful when you need to run a specific routine and then return to where you came from and continue with other instructions.
- call doWork
- call printInfo
- call doCalculation

**RET**
The RET instruction sets the instructionpointer back to the instruction that comes right after the CALL instruction.

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
