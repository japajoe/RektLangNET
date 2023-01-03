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

        public IntPtr GetBuffer()
        {
            return VoltNative.volt_stack_get_buffer(handle);
        }

        public IntPtr GetTop()
        {
            return VoltNative.volt_stack_get_top(handle);
        }

        public DataType GetTopType()
        {
            return VoltNative.volt_stack_get_top_type(handle);
        }

        public UInt64 GetCapacity()
        {
            return VoltNative.volt_stack_get_capacity(handle);
        }

        public UInt64 GetSize()
        {
            return VoltNative.volt_stack_get_size(handle);
        }

        public UInt64 GetCount()
        {
            return VoltNative.volt_stack_get_count(handle);
        }
    }
}
