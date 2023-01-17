using System;

namespace VoltLangNET
{
    public unsafe struct Stack
    {
        private StackPointer handle;

        public StackPointer Handle
        {
            get
            {
                return handle;
            }
        }

        public Stack(StackPointer handle)
        {
            this.handle = handle;
        }

        public void Clear()
        {
            VoltNative.volt_stack_clear(handle);
        }

        public bool Push(VoltObject obj)
        {
            return VoltNative.volt_stack_push(handle, obj);
        }

        public bool Push(Int64 value)
        {
            VoltObject obj = new VoltObject(value);
            return VoltNative.volt_stack_push(handle, obj);
        }

        public bool Push(UInt64 value)
        {
            VoltObject obj = new VoltObject(value);
            return VoltNative.volt_stack_push(handle, obj);
        }

        public bool Push(double value)
        {
            VoltObject obj = new VoltObject(value);
            return VoltNative.volt_stack_push(handle, obj);
        }

        public bool Push(IntPtr value, DataType type)
        {
            VoltObject obj = new VoltObject(value.ToPointer(), type);
            return VoltNative.volt_stack_push(handle, obj);
        }        

        public bool Pop(out VoltObject obj)
        {
            return VoltNative.volt_stack_pop(handle, out obj);
        }

        public bool Pop(out Int64 value)
        {
            value = default;
            VoltObject obj;
            if(VoltNative.volt_stack_pop(handle, out obj))
            {
                if(obj.type == DataType.Int64Pointer)
                    value = *obj.as_int64_pointer;
                else
                    value = obj.as_int64;
                return true;
            }

            return false;
        }

        public bool Pop(out UInt64 value)
        {
            value = default;
            VoltObject obj;
            if(VoltNative.volt_stack_pop(handle, out obj))
            {
                if(obj.type == DataType.UInt64Pointer)
                    value = *obj.as_uint64_pointer;
                else
                    value = obj.as_uint64;
                return true;
            }

            return false;
        }

        public bool Pop(out double value)
        {
            value = default;
            VoltObject obj;
            if(VoltNative.volt_stack_pop(handle, out obj))
            {
                if(obj.type == DataType.DoublePointer)
                    value = *obj.as_double_pointer;
                else
                    value = obj.as_double;
                return true;
            }

            return false;
        }

        public bool Pop(out IntPtr value, out DataType type)
        {
            value = default;
            type = DataType.Undefined;
            VoltObject obj;
            if(VoltNative.volt_stack_pop(handle, out obj))
            {
                value = new IntPtr(obj.as_void_pointer);
                type = obj.type;
                return true;
            }

            return false;
        }        

        /// <summary>
        /// Gets the type of the objects that is on the top of the stack.
        /// </summary>
        /// <returns>The type of the objects that is on the top of the stack</returns>
        public DataType GetTopType()
        {
            return VoltNative.volt_stack_get_top_type(handle);
        }

        public bool CheckType(DataType type, Int64 index)
        {
            return VoltNative.volt_stack_check_type(handle, type, index);
        }

        public bool CheckTypesTopToBottom(params DataType[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                int index = 0 - (i+1);

                if(!CheckType(types[i], index))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckTypesBottomToTop(params DataType[] types)
        {
            for (int i = types.Length - 1; i >= 0; i--)
            {
                int index = 0 - (i+1);

                if(!CheckType(types[i], index))
                {
                    return false;
                }
            }

            return true;
        }        

        /// <summary>
        /// Gets the total capacity of the stack in number of objects.
        /// </summary>
        /// <returns>The total capacity of the stack in number of objects.</returns>
        public UInt64 GetCapacity()
        {
            return VoltNative.volt_stack_get_capacity(handle);
        }        

        /// <summary>
        /// Gets the number of objects currently on the stack.
        /// </summary>
        /// <returns>The number of objects currently on the stack</returns>
        public UInt64 Getcount()
        {
            return VoltNative.volt_stack_get_count(handle);
        }
    }
}
