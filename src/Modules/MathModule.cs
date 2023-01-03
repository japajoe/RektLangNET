using System;

namespace VoltLangNET
{
    public class MathModule : IModule
    {
        private static byte[] buffer = new byte[8];
        private static VoltVMFunction cos;
        private static VoltVMFunction sin;
        private static VoltVMFunction tan;
        private static VoltVMFunction asin;
        private static VoltVMFunction atan;
        private static VoltVMFunction atan2;
        private static VoltVMFunction cosh;
        private static VoltVMFunction sinh;
        private static VoltVMFunction tanh;
        private static VoltVMFunction asinh;
        private static VoltVMFunction atanh;
        private static VoltVMFunction exp;
        private static VoltVMFunction log;
        private static VoltVMFunction log10;
        private static VoltVMFunction pow;
        private static VoltVMFunction sqrt;
        private static VoltVMFunction ceil;
        private static VoltVMFunction floor;
        private static VoltVMFunction trunc;
        private static VoltVMFunction round;
        private static VoltVMFunction remainder;
        private static VoltVMFunction max;
        private static VoltVMFunction min;
        private static VoltVMFunction abs;

        public MathModule()
        {
            cos = Cos;
            sin = Sin;
            tan = Tan;
            asin = Asin;
            atan = Atan;
            atan2 = Atan2;
            cosh = Cosh;
            sinh = Sinh;
            tanh = Tanh;
            asinh = Asinh;
            atanh = Atanh;
            exp = Exp;
            log = Log;
            log10 = Log10;
            pow = Pow;
            sqrt = Sqrt;
            ceil = Ceil;
            floor = Floor;
            trunc = Trunc;
            round = Round;
            remainder = Remainder;
            max = Max;
            min = Min;
            abs = Abs;
        }

        public void Register()
        {
            VirtualMachine.RegisterFunction("cos", cos);
            VirtualMachine.RegisterFunction("sin", sin);
            VirtualMachine.RegisterFunction("tan", tan);
            VirtualMachine.RegisterFunction("asin", asin);
            VirtualMachine.RegisterFunction("atan", atan);
            VirtualMachine.RegisterFunction("atan2", atan2);
            VirtualMachine.RegisterFunction("cosh", cosh);
            VirtualMachine.RegisterFunction("sinh", sinh);
            VirtualMachine.RegisterFunction("tanh", tanh);
            VirtualMachine.RegisterFunction("asinh", asinh);
            VirtualMachine.RegisterFunction("atanh", atanh);
            VirtualMachine.RegisterFunction("exp", exp);
            VirtualMachine.RegisterFunction("log", log);
            VirtualMachine.RegisterFunction("log10", log10);
            VirtualMachine.RegisterFunction("pow", pow);
            VirtualMachine.RegisterFunction("sqrt", sqrt);
            VirtualMachine.RegisterFunction("ceil", ceil);
            VirtualMachine.RegisterFunction("floor", floor);
            VirtualMachine.RegisterFunction("trunc", trunc);
            VirtualMachine.RegisterFunction("round", round);
            VirtualMachine.RegisterFunction("remainder", remainder);
            VirtualMachine.RegisterFunction("max", max);
            VirtualMachine.RegisterFunction("min", min);
            VirtualMachine.RegisterFunction("abs", abs);
        }

        public void Dispose()
        {
            
        }        

        private static int Cos(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Cos(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Sin(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(stack.GetTopType() != DataType.Double && stack.GetTopType() != DataType.Int64)
                return -1;

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
                return -1;

            double result = Math.Sin(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Tan(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Tan(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Asin(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Asin(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Atan(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Atan(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Atan2(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double y, out ulong offset))
            {
                return -1;
            }

            if(!stack.TryPopAsDouble(buffer, out double x, out offset))
            {
                return -1;
            }         

            double result = Math.Atan2(x, y);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Cosh(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Cosh(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Sinh(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Sinh(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Tanh(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Tanh(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Asinh(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Asinh(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Atanh(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Atanh(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Exp(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Exp(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Log(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Log(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Log10(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Log10(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Pow(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double y, out ulong offset))
            {
                return -1;
            }

            if(!stack.TryPopAsDouble(buffer, out double x, out offset))
            {
                return -1;
            }          

            double result = Math.Pow(x, y);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Sqrt(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Sqrt(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Ceil(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Ceiling(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Floor(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Floor(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Trunc(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Truncate(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Round(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Round(value);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Remainder(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double y, out ulong offset))
            {
                return -1;
            }

            if(!stack.TryPopAsDouble(buffer, out double x, out offset))
            {
                return -1;
            }

            double result = x % y;

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Max(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double y, out ulong offset))
            {
                return -1;
            }

            if(!stack.TryPopAsDouble(buffer, out double x, out offset))
            {
                return -1;
            }           

            double result = Math.Max(x, y);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Min(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double y, out ulong offset))
            {
                return -1;
            }

            if(!stack.TryPopAsDouble(buffer, out double x, out offset))
            {
                return -1;
            }        

            double result = Math.Min(x, y);

            stack.PushDouble(result, out offset);

            return 0;
        }

        private static int Abs(StackPointer sp)
        {
            Stack stack = new Stack(sp);

            if(!stack.TryPopAsDouble(buffer, out double value, out ulong offset))
            {
                return -1;
            }

            double result = Math.Abs(value);

            stack.PushDouble(result, out offset);

            return 0;
        }
    }
}