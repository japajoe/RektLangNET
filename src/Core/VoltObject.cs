using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    using UInt8 = Byte;
    using Int8 = SByte;

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct VoltObject
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

        public VoltObject() 
        {
            type = DataType.Undefined;
        }

        public VoltObject(UInt8 value)
        {
            as_uint8 = value;
            type = DataType.UInt8;
        }

        public VoltObject(UInt16 value)
        {
            as_uint16 = value;
            type = DataType.UInt16;
        }

        public VoltObject(UInt32 value)
        {
            as_uint32 = value;
            type = DataType.UInt32;
        }

        public VoltObject(UInt64 value)
        {
            as_uint64 = value;
            type = DataType.UInt64;
        }

        public VoltObject(Int8 value)
        {
            as_int8 = value;
            type = DataType.Int8;
        }

        public VoltObject(Int16 value)
        {
            as_int16 = value;
            type = DataType.Int16;
        }

        public VoltObject(Int32 value)
        {
            as_int32 = value;
            type = DataType.Int32;
        }

        public VoltObject(Int64 value)
        {
            as_int64 = value;
            type = DataType.Int64;
        }

        public VoltObject(double value)
        {
            as_double = value;
            type = DataType.Double;
        }

        public VoltObject(UInt8 *value)
        {
            as_uint8_pointer = value;
            type = DataType.UInt8Pointer;
        }

        public VoltObject(UInt16 *value)
        {
            as_uint16_pointer = value;
            type = DataType.UInt16Pointer;
        }

        public VoltObject(UInt32 *value)
        {
            as_uint32_pointer = value;
            type = DataType.UInt32Pointer;
        }

        public VoltObject(UInt64 *value)
        {
            as_uint64_pointer = value;
            type = DataType.UInt64Pointer;
        }

        public VoltObject(Int8 *value)
        {
            as_int8_pointer = value;
            type = DataType.Int8Pointer;
        }

        public VoltObject(Int16 *value)
        {
            as_int16_pointer = value;
            type = DataType.Int16Pointer;
        }

        public VoltObject(Int32 *value)
        {
            as_int32_pointer = value;
            type = DataType.Int32Pointer;
        }

        public VoltObject(Int64 *value)
        {
            as_int64_pointer = value;
            type = DataType.Int64Pointer;
        }

        public VoltObject(double *value)
        {
            as_double_pointer = value;
            type = DataType.DoublePointer;
        }

        public VoltObject(void *value)
        {
            as_void_pointer = value;
            type = DataType.VoidPointer;
        }

        public VoltObject(void *value, DataType type)
        {
            as_void_pointer = value;
            this.type = type;
        }
    }    
}