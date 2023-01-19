using System;
using System.Runtime.InteropServices;

namespace RektLangNET
{
    public delegate int RektVMFunction(StackPointer stack);

    public static unsafe class RektNative
    {
        private const string LIBRARY_NAME = "rekt";    

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_modules_load();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_modules_dispose();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_function_register(string name, RektVMFunction fn_ptr);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern AssemblyPointer rekt_assembly_create();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_assembly_destroy(AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 rekt_assembly_get_num_instructions(AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_assembly_get_symbol_info(AssemblyPointer assembly, string name, out UInt64 startOffset, out UInt64 size, out DataType DataType);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_assembly_get_label_info(AssemblyPointer assembly, string name, out UInt64 offset);        

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr rekt_assembly_get_symbols(AssemblyPointer assembly, out UInt64 count);

        public static string[] rekt_assembly_get_symbols(AssemblyPointer assembly)
        {
            IntPtr ptrStrArray = rekt_assembly_get_symbols(assembly, out UInt64 count);

            if(ptrStrArray == IntPtr.Zero)
                return null;

            IntPtr[] pointers = new IntPtr[count];
            string[] symbols = new string[count];

            Marshal.Copy(ptrStrArray, pointers, 0, (int)count);

            for (int i = 0; i < pointers.Length; i++)
            {
                symbols[i] = Marshal.PtrToStringUTF8(pointers[i]);
                rekt_free_char_pointer(pointers[i]);
            }

            rekt_free_char_pointer(ptrStrArray);

            return symbols;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr rekt_assembly_get_labels(AssemblyPointer assembly, out UInt64 count);

        public static string[] rekt_assembly_get_labels(AssemblyPointer assembly)
        {
            IntPtr ptrStrArray = rekt_assembly_get_labels(assembly, out UInt64 count);

            if(ptrStrArray == IntPtr.Zero)
                return null;

            IntPtr[] pointers = new IntPtr[count];
            string[] labels = new string[count];

            Marshal.Copy(ptrStrArray, pointers, 0, (int)count);

            for (int i = 0; i < pointers.Length; i++)
            {
                labels[i] = Marshal.PtrToStringUTF8(pointers[i]);
                rekt_free_char_pointer(pointers[i]);
            }

            rekt_free_char_pointer(ptrStrArray);

            return labels;            
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern OpCode rekt_assembly_get_instruction_opcode(AssemblyPointer assembly, UInt64 offset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern VirtualMachinePointer rekt_virtual_machine_create(UInt64 stackCapacity);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_virtual_machine_destroy(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_virtual_machine_load_assembly(VirtualMachinePointer vm, AssemblyPointer assembly);
        
        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_virtual_machine_reset(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_virtual_machine_set_instruction_pointer(VirtualMachinePointer vm, UInt64 offset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 rekt_virtual_machine_get_instruction_pointer(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern ExecutionStatus rekt_virtual_machine_run(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern ExecutionStatus rekt_virtual_machine_call(VirtualMachinePointer vm, UInt64 labelOffset);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr rekt_virtual_machine_get_registers(VirtualMachinePointer vm, out UInt64 size);

        public static bool rekt_virtual_machine_get_registers(VirtualMachinePointer vm, out Span<RektObject> buffer)
        {
            buffer = default;

            IntPtr ptr = rekt_virtual_machine_get_registers(vm, out UInt64 size);

            if (ptr == IntPtr.Zero)
                return false;

            buffer = new Span<RektObject>(ptr.ToPointer(), (int)size);

            return true;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern StackPointer rekt_virtual_machine_get_stack(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 rekt_virtual_machine_get_compare_flag(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 rekt_virtual_machine_get_zero_flag(VirtualMachinePointer vm);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompilerPointer rekt_compiler_create();

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_compiler_destroy(CompilerPointer compiler);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_compile_from_text(CompilerPointer compiler, string source, AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_compile_from_file(CompilerPointer compiler, string filepath, AssemblyPointer assembly);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rekt_stack_clear(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_stack_push(StackPointer stack, RektObject obj);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_stack_pop(StackPointer stack, out RektObject obj);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern DataType rekt_stack_get_top_type(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool rekt_stack_check_type(StackPointer stack, DataType type, Int64 index);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 rekt_stack_get_capacity(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 rekt_stack_get_count(StackPointer stack);

        [DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl)]
        private static extern void rekt_free_char_pointer(IntPtr ptr);
    }
}
