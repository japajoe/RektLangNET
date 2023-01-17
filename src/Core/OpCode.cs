namespace VoltLangNET
{
    public enum OpCode : byte
    {
        HLT = 0,
        PUSH,
        POP,
        MOV,
        INC,
        DEC,
        ADD,
        SUB,
        MUL,
        DIV,
        CMP,
        JMP,
        JG,
        JGE,
        JL,
        JLE,
        JE,
        JNE,
        JZ,
        JNZ,
        CALL,
        RET,
        NOP,
        NUM_OPCODES
    }
}