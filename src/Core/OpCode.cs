namespace VoltLangNET
{
    public enum OpCode : byte
    {
        HLT = 0,
        PSH = 1,
        POP = 2,
        MOV = 3,
        INC = 4,
        DEC = 5,
        Add = 6,
        SUB = 7,
        MUL = 8,
        DIV = 9,
        CMP = 10,
        JMP = 11,
        JG = 12,
        JGE = 13,
        JL = 14,
        JLE = 15,
        JE = 16,
        JNE = 17,
        JZ = 18,
        JNZ = 19,
        CAL = 20,
        RET = 21,
        NOP = 22,
        NUM_OPCODES = 23        
    }    
}