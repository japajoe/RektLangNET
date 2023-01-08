using System;
using System.Runtime.InteropServices;

namespace VoltLangNET
{
    public delegate int VoltVMFunction(StackPointer stack);

    public enum DataType : byte
    {
        UInt8 = 0,
        Int8 = 1,
        UInt16 = 2,
        Int16 = 3,
        UInt32 = 4,
        Int32 = 5,
        UInt64 = 6,
        Int64 = 7,
        Single = 8,
        Double = 9,
        Pointer = 10,
        Unknown = 11
    }

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

    public static unsafe class VoltNative
    {
        private const string LIBRARY_NAME = "volt";    

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_modules_load();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_modules_dispose();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_function_register(string name, VoltVMFunction fn_ptr);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern AssemblyPointer volt_assembly_create();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_assembly_destroy(AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 volt_assembly_get_num_instructions(AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_assembly_get_symbol_info(AssemblyPointer assembly, string name, out UInt64 startOffset, out UInt64 size, out DataType DataType);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_assembly_get_label_info(AssemblyPointer assembly, string name, out UInt64 offset);        

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr volt_assembly_get_symbols(AssemblyPointer assembly, out UInt64 count);

        public static string[] volt_assembly_get_symbols(AssemblyPointer assembly)
        {
            IntPtr ptrStrArray = volt_assembly_get_symbols(assembly, out UInt64 count);

            if(ptrStrArray == IntPtr.Zero)
                return null;

            IntPtr[] pointers = new IntPtr[count];
            string[] symbols = new string[count];

            Marshal.Copy(ptrStrArray, pointers, 0, (int)count);

            for (int i = 0; i < pointers.Length; i++)
            {
                symbols[i] = Marshal.PtrToStringUTF8(pointers[i]);
                volt_free_char_pointer(pointers[i]);
            }

            volt_free_char_pointer(ptrStrArray);

            return symbols;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr volt_assembly_get_labels(AssemblyPointer assembly, out UInt64 count);

        public static string[] volt_assembly_get_labels(AssemblyPointer assembly)
        {
            IntPtr ptrStrArray = volt_assembly_get_labels(assembly, out UInt64 count);

            if(ptrStrArray == IntPtr.Zero)
                return null;

            IntPtr[] pointers = new IntPtr[count];
            string[] labels = new string[count];

            Marshal.Copy(ptrStrArray, pointers, 0, (int)count);

            for (int i = 0; i < pointers.Length; i++)
            {
                labels[i] = Marshal.PtrToStringUTF8(pointers[i]);
                volt_free_char_pointer(pointers[i]);
            }

            volt_free_char_pointer(ptrStrArray);

            return labels;            
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern OpCode volt_assembly_get_instruction_opcode(AssemblyPointer assembly, UInt64 offset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern VirtualMachinePointer volt_virtual_machine_create(UInt64 stackCapacity);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_virtual_machine_destroy(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_virtual_machine_load_assembly(VirtualMachinePointer vm, AssemblyPointer assembly);
        
        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_virtual_machine_reset(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_virtual_machine_set_instruction_pointer(VirtualMachinePointer vm, UInt64 offset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 volt_virtual_machine_get_instruction_pointer(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern ExecutionStatus volt_virtual_machine_run(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern ExecutionStatus volt_virtual_machine_call(VirtualMachinePointer vm, UInt64 labelOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr volt_virtual_machine_get_registers(VirtualMachinePointer vm, out UInt64 size);

        public static unsafe bool volt_virtual_machine_get_registers(VirtualMachinePointer vm, out Span<byte> buffer)
        {
            buffer = default;

            IntPtr ptr = volt_virtual_machine_get_registers(vm, out UInt64 size);

            if (ptr == IntPtr.Zero)
                return false;

            buffer = new Span<byte>(ptr.ToPointer(), (int)size);

            return true;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern StackPointer volt_virtual_machine_get_stack(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 volt_virtual_machine_get_compare_flag(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 volt_virtual_machine_get_zero_flag(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompilerPointer volt_compiler_create();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_compiler_destroy(CompilerPointer compiler);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_compile_from_text(CompilerPointer compiler, string source, AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_compile_from_file(CompilerPointer compiler, string filepath, AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_stack_clear(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe bool volt_stack_push(StackPointer stack, byte* data, DataType type, out UInt64 stackOffset);

        public static unsafe bool volt_stack_push(StackPointer stack, byte[] data, DataType type, out UInt64 stackOffset)
        {
            fixed(byte* ptr = &data[0])
            {
                return volt_stack_push(stack, ptr, type, out stackOffset);
            }
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_stack_push_double(StackPointer stack, double value, out UInt64 stackOffset);        

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_stack_push_int64(StackPointer stack, Int64 value, out UInt64 stackOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_stack_push_uint64(StackPointer stack, UInt64 value, out UInt64 stackOffset);

        //[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        //public static extern unsafe bool volt_stack_push_string(StackPointer stack, char* value, out UInt64 stackOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe bool volt_stack_push_string(StackPointer stack, [MarshalAs(UnmanagedType.LPStr )] string value, out UInt64 stackOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe bool volt_stack_push_string(StackPointer stack, UIntPtr value, out UInt64 stackOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe bool volt_stack_pop(StackPointer stack, byte* target, out UInt64 stackOffset);

        public static unsafe bool volt_stack_pop(StackPointer stack, byte[] target, out UInt64 stackOffset)
        {
            fixed(byte* ptr = &target[0])
            {
                return volt_stack_pop(stack, ptr, out stackOffset);
            }
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_stack_pop_with_count(StackPointer stack, UInt64 count, out UInt64 stackOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr volt_stack_get_buffer(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr volt_stack_get_top(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool volt_stack_check_type(StackPointer stack, DataType type, Int64 index);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern DataType volt_stack_get_top_type(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 volt_stack_get_capacity(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 volt_stack_get_size(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 volt_stack_get_count(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void volt_free_char_pointer(IntPtr ptr);

        /// <summary>
        /// Gets the base address of a given module (shared library) that is loaded in the running process.
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="address">The address of the module</param>
        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_get_module_address(string name, out UInt64 address);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr volt_memory_alloc(UInt64 size);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr volt_memory_set(IntPtr src, int value, UInt64 size);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr volt_memory_copy(IntPtr src, IntPtr dest, UInt64 size);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void volt_memory_free(IntPtr ptr);
    }
}
