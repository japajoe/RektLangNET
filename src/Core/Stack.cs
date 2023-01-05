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

        public bool Push(byte[] data, DataType type, out UInt64 stackOffset)
        {
            return VoltNative.volt_stack_push(handle, data, type, out stackOffset);
        }

        public bool PushDouble(double value, out UInt64 stackOffset)
        {
            return VoltNative.volt_stack_push_double(handle, value, out stackOffset);
        }        

        public bool PushInt64(Int64 value, out UInt64 stackOffset)
        {
            return VoltNative.volt_stack_push_int64(handle, value, out stackOffset);
        }

        public bool PushUInt64(UInt64 value, out UInt64 stackOffset)
        {
            return VoltNative.volt_stack_push_uint64(handle, value, out stackOffset);
        }

        public bool PushString(IntPtr value, out UInt64 stackOffset)
        {
            char* ptr = (char*)value.ToPointer();            
            return VoltNative.volt_stack_push_string(handle, ptr, out stackOffset);
        }          

        public bool Pop(byte[] target, out UInt64 stackOffset)
        {
            return VoltNative.volt_stack_pop(handle, target, out stackOffset);
        }

        /// <summary>
        /// Pops x number of items off the stack.
        /// </summary>
        /// <param name="count">The number if items to pop</param>
        /// <param name=""></param>
        /// <returns>True on succes or when there is nothing to pop, false on failure</returns>
        public bool Pop(UInt64 count, out UInt64 stackOffset)
        {
            if (GetCount() == 0)
            {
                stackOffset = 0;
                return true;
            }

            return VoltNative.volt_stack_pop_with_count(handle, count, out stackOffset);
        }

        public bool PopDouble(byte[] target, out double value, out UInt64 stackOffset)
        {
            value = 0;
            stackOffset = 0;

            if(VoltNative.volt_stack_pop(handle, target, out stackOffset))
            {
                value = BitConverter.ToDouble(target, 0);
                return true;
            }

            return false;
        }

        public bool PopUInt64(byte[] target, out UInt64 value, out UInt64 stackOffset)
        {
            value = 0;

            if(VoltNative.volt_stack_pop(handle, target, out stackOffset))
            {
                value = BitConverter.ToUInt64(target, 0);
                return true;
            }

            return false;
        }

        public bool PopInt64(byte[] target, out Int64 value, out UInt64 stackOffset)
        {
            value = 0;

            if(VoltNative.volt_stack_pop(handle, target, out stackOffset))
            {
                value = BitConverter.ToInt64(target, 0);
                return true;
            }

            return false;
        }

        public bool TryPopAsDouble(byte[] target, out double value, out UInt64 stackOffset)
        {
            value = 0;
            stackOffset = 0;

            DataType type = GetTopType();

            switch(type)
            {
                case DataType.Double:
                {
                    if(!PopDouble(target, out value, out stackOffset))
                    {
                        return false;
                    }

                    return true;
                }
                case DataType.UInt64:
                {
                    if(!PopUInt64(target, out UInt64 v, out stackOffset))
                    {
                        return false;
                    }


                    value = (double)v;
                    return true;
                }
                case DataType.Int64:
                {
                    if(!PopInt64(target, out Int64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = (double)v;
                    return true;
                }
                default:
                    return false;
            }
        }

        public bool TryPopAsUInt64(byte[] target, out UInt64 value, out UInt64 stackOffset)
        {
            value = 0;
            stackOffset = 0;

            DataType type = GetTopType();

            switch(type)
            {
                case DataType.Double:
                {
                    if(!PopDouble(target, out double v, out stackOffset))
                    {
                        return false;
                    }

                    value = (UInt64)v;
                    return true;
                }
                case DataType.UInt64:
                {
                    if(!PopUInt64(target, out value, out stackOffset))
                    {
                        return false;
                    }

                    return true;
                }
                case DataType.Int64:
                {
                    if(!PopInt64(target, out Int64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = (UInt64)v;
                    return true;
                }
                default:
                    return false;
            }
        }

        public bool TryPopAsInt64(byte[] target, out Int64 value, out UInt64 stackOffset)
        {
            value = 0;
            stackOffset = 0;

            DataType type = GetTopType();

            switch(type)
            {
                case DataType.Double:
                {
                    if(!PopDouble(target, out double v, out stackOffset))
                    {
                        return false;
                    }

                    value = (Int64)v;
                    return true;
                }
                case DataType.UInt64:
                {
                    if(!PopUInt64(target, out UInt64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = (Int64)v;
                    return true;
                }
                case DataType.Int64:
                {
                    if(!PopInt64(target, out value, out stackOffset))
                    {
                        return false;
                    }

                    return true;
                }
                default:
                    return false;
            }
        }

        public bool TryPopAsString(byte[] target, out string value, out UInt64 stackOffset)
        {
            value = default;
            stackOffset = 0;

            DataType type = GetTopType();

            switch(type)
            {
                case DataType.Double:
                {
                    if(!PopDouble(target, out double v, out stackOffset))
                    {
                        return false;
                    }

                    value = v.ToString();
                    return true;
                }
                case DataType.UInt64:
                {
                    if(!PopUInt64(target, out UInt64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = v.ToString();
                    return true;
                }
                case DataType.Int64:
                {
                    if(!PopInt64(target, out Int64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = v.ToString();
                    return true;
                }
                case DataType.Pointer:
                {
                    if(!Pop(target, out ulong offset))
                    {
                        return false;
                    }

                    value = NullTerminatedString.GetString(target);
                    return true;
                }
                default:
                    return false;
            }        
        }

        public bool TryPopAsObject(byte[] target, out object value, out UInt64 stackOffset)
        {
            value = default;
            stackOffset = 0;

            DataType type = GetTopType();

            switch(type)
            {
                case DataType.Double:
                {
                    if(!PopDouble(target, out double v, out stackOffset))
                    {
                        return false;
                    }

                    value = v;
                    return true;
                }
                case DataType.UInt64:
                {
                    if(!PopUInt64(target, out UInt64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = v;
                    return true;
                }
                case DataType.Int64:
                {
                    if(!PopInt64(target, out Int64 v, out stackOffset))
                    {
                        return false;
                    }

                    value = v;
                    return true;
                }
                case DataType.Pointer:
                {
                    if(!Pop(target, out ulong offset))
                    {
                        return false;
                    }

                    value = NullTerminatedString.GetString(target);
                    return true;
                }
                default:
                    return false;
            }        
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
        /// Gets a pointer to the stack buffer.
        /// </summary>
        /// <returns>A pointer to the stack buffer</returns>
        public IntPtr GetBuffer()
        {
            return VoltNative.volt_stack_get_buffer(handle);
        }

        /// <summary>
        /// Gets a pointer to the item on the top of the stack.
        /// </summary>
        /// <returns>A pointer to the item on the top of the stack.</returns>
        public IntPtr GetTop()
        {
            return VoltNative.volt_stack_get_top(handle);
        }

        /// <summary>
        /// Gets the type of the item that is on the top of the stack.
        /// </summary>
        /// <returns>The type of the item that is on the top of the stack</returns>
        public DataType GetTopType()
        {
            return VoltNative.volt_stack_get_top_type(handle);
        }

        /// <summary>
        /// Gets the total capacity of the stack in number of bytes.
        /// </summary>
        /// <returns>The total capacity of the stack in number of bytes.</returns>
        public UInt64 GetCapacity()
        {
            return VoltNative.volt_stack_get_capacity(handle);
        }

        /// <summary>
        /// Gets the number of bytes that currently is in the stack.
        /// </summary>
        /// <returns>The number of bytes that currently is in the stack</returns>
        public UInt64 GetSize()
        {
            return VoltNative.volt_stack_get_size(handle);
        }

        /// <summary>
        /// Gets the number of items currently on the stack.
        /// </summary>
        /// <returns>The number of items currently on the stack</returns>
        public UInt64 GetCount()
        {
            return VoltNative.volt_stack_get_count(handle);
        }
    }
}
