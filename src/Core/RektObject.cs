using System;
using System.Runtime.InteropServices;

namespace RektLangNET
{
    using UInt8 = Byte;
    using Int8 = SByte;

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct RektObject
    {
        [FieldOffset(0)] public DataType type;
        [FieldOffset(8)] public UInt8 as_uint8;
        [FieldOffset(8)] public UInt16 as_uint16;
        [FieldOffset(8)] public UInt32 as_uint32;
        [FieldOffset(8)] public UInt64 as_uint64;
        [FieldOffset(8)] public Int8 as_int8;
        [FieldOffset(8)] public Int16 as_int16;
        [FieldOffset(8)] public Int32 as_int32;
        [FieldOffset(8)] public Int64 as_int64;
        [FieldOffset(8)] public double as_double;
        [FieldOffset(8)] public UInt8 *as_uint8_pointer;
        [FieldOffset(8)] public UInt16 *as_uint16_pointer;
        [FieldOffset(8)] public UInt32 *as_uint32_pointer;
        [FieldOffset(8)] public UInt64 *as_uint64_pointer;
        [FieldOffset(8)] public Int8 *as_int8_pointer;
        [FieldOffset(8)] public Int16 *as_int16_pointer;
        [FieldOffset(8)] public Int32 *as_int32_pointer;
        [FieldOffset(8)] public Int64 *as_int64_pointer;
        [FieldOffset(8)] public double *as_double_pointer;
        [FieldOffset(8)] public Int8 *as_char_pointer; //Can't use char* since a char is 2 bytes in C#
        [FieldOffset(8)] public void *as_void_pointer;

        public RektObject() 
        {
            type = DataType.Undefined;
        }

        public RektObject(UInt8 value)
        {
            as_uint8 = value;
            type = DataType.UInt8;
        }

        public RektObject(UInt16 value)
        {
            as_uint16 = value;
            type = DataType.UInt16;
        }

        public RektObject(UInt32 value)
        {
            as_uint32 = value;
            type = DataType.UInt32;
        }

        public RektObject(UInt64 value)
        {
            as_uint64 = value;
            type = DataType.UInt64;
        }

        public RektObject(Int8 value)
        {
            as_int8 = value;
            type = DataType.Int8;
        }

        public RektObject(Int16 value)
        {
            as_int16 = value;
            type = DataType.Int16;
        }

        public RektObject(Int32 value)
        {
            as_int32 = value;
            type = DataType.Int32;
        }

        public RektObject(Int64 value)
        {
            as_int64 = value;
            type = DataType.Int64;
        }

        public RektObject(double value)
        {
            as_double = value;
            type = DataType.Double;
        }

        public RektObject(UInt8 *value)
        {
            as_uint8_pointer = value;
            type = DataType.UInt8Pointer;
        }

        public RektObject(UInt16 *value)
        {
            as_uint16_pointer = value;
            type = DataType.UInt16Pointer;
        }

        public RektObject(UInt32 *value)
        {
            as_uint32_pointer = value;
            type = DataType.UInt32Pointer;
        }

        public RektObject(UInt64 *value)
        {
            as_uint64_pointer = value;
            type = DataType.UInt64Pointer;
        }

        public RektObject(Int8 *value)
        {
            as_int8_pointer = value;
            type = DataType.Int8Pointer;
        }

        public RektObject(Int16 *value)
        {
            as_int16_pointer = value;
            type = DataType.Int16Pointer;
        }

        public RektObject(Int32 *value)
        {
            as_int32_pointer = value;
            type = DataType.Int32Pointer;
        }

        public RektObject(Int64 *value)
        {
            as_int64_pointer = value;
            type = DataType.Int64Pointer;
        }

        public RektObject(double *value)
        {
            as_double_pointer = value;
            type = DataType.DoublePointer;
        }

        public RektObject(void *value)
        {
            as_void_pointer = value;
            type = DataType.VoidPointer;
        }

        public RektObject(void *value, DataType type)
        {
            as_void_pointer = value;
            this.type = type;
        }

        public bool IsPointerType()
        {
            switch(type)
            {
                case DataType.Int8Pointer:
                case DataType.UInt8Pointer:
                case DataType.Int16Pointer:
                case DataType.UInt16Pointer:
                case DataType.Int32Pointer:
                case DataType.UInt32Pointer:
                case DataType.Int64Pointer:
                case DataType.UInt64Pointer:
                case DataType.DoublePointer:               
                case DataType.CharPointer:
                case DataType.VoidPointer:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsNumericType()
        {
            switch(type)
            {
                case DataType.Int8:
                case DataType.UInt8:
                case DataType.Int16:
                case DataType.UInt16:
                case DataType.Int32:
                case DataType.UInt32:
                case DataType.Int64:
                case DataType.UInt64:
                case DataType.Double:               
                    return true;
                default:
                    return false;
            }
        }         
    }    
}