namespace RektLangNET
{
    public enum ExecutionStatus
    {
        Ok = 0,
        Done,
        IllegalOperation,
        IllegalJump,
        StackOverflow,
        StackUnderflow,
        DivisionByZero,
        NotImplemented,
        Error
    }
}