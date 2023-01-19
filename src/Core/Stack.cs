using System;

namespace RektLangNET
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
            RektNative.rekt_stack_clear(handle);
        }

        public bool Push(RektObject obj)
        {
            return RektNative.rekt_stack_push(handle, obj);
        }

        public bool Push(Int64 value)
        {
            RektObject obj = new RektObject(value);
            return RektNative.rekt_stack_push(handle, obj);
        }

        public bool Push(UInt64 value)
        {
            RektObject obj = new RektObject(value);
            return RektNative.rekt_stack_push(handle, obj);
        }

        public bool Push(double value)
        {
            RektObject obj = new RektObject(value);
            return RektNative.rekt_stack_push(handle, obj);
        }

        public bool Push(IntPtr value, DataType type)
        {
            RektObject obj = new RektObject(value.ToPointer(), type);
            return RektNative.rekt_stack_push(handle, obj);
        }        

        public bool Pop(out RektObject obj)
        {
            return RektNative.rekt_stack_pop(handle, out obj);
        }

        public bool Pop(out Int64 value)
        {
            value = default;
            RektObject obj;
            if(RektNative.rekt_stack_pop(handle, out obj))
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
            RektObject obj;
            if(RektNative.rekt_stack_pop(handle, out obj))
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
            RektObject obj;
            if(RektNative.rekt_stack_pop(handle, out obj))
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
            RektObject obj;
            if(RektNative.rekt_stack_pop(handle, out obj))
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
            return RektNative.rekt_stack_get_top_type(handle);
        }

        public bool CheckType(DataType type, Int64 index)
        {
            return RektNative.rekt_stack_check_type(handle, type, index);
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
            return RektNative.rekt_stack_get_capacity(handle);
        }        

        /// <summary>
        /// Gets the number of objects currently on the stack.
        /// </summary>
        /// <returns>The number of objects currently on the stack</returns>
        public UInt64 Getcount()
        {
            return RektNative.rekt_stack_get_count(handle);
        }
    }
}
