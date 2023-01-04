namespace VoltLangNET
{
    public enum OpCode : byte
    {
        Halt = 0,
        Push,
        Pop,
        Move,
        Increment,
        Decrement,
        Add,
        Subtract,
        Multiply,
        Divide,
        Compare,
        Jump,
        JumpIfGreaterThan,
        JumpIfGreaterThanOrEqual,
        JumpIfLessThan,
        JumpIfLessThanOrEqual,
        JumpIfEqual,
        JumpIfNotEqual,
        JumpIfZero,
        JumpIfNotZero,
        Call,
        Return,
        NOP,
        NUM_OPCODES
    }
}