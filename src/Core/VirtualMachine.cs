using System;

namespace VoltLangNET
{
    public class VirtualMachine : IDisposable
    {
        private VirtualMachinePointer handle;

        public VirtualMachinePointer Handle
        {
            get
            {
                return handle;
            }
        }

        /// <summary>
        /// Creates a new VirtualMachine instance.
        /// </summary>
        /// <param name="stackCapacity">The capacity of the stack in number of bytes. Minimum size is 1024 and must be a power of 2.</param>
        public VirtualMachine(UInt64 stackCapacity = 8192)
        {
            handle = VoltNative.volt_virtual_machine_create(stackCapacity);
        }

        /// <summary>
        /// Loads an assembly into the virtual machine.
        /// </summary>
        /// <param name="assembly">The target assembly</param>
        /// <returns>True on success, false on failure.</returns>
        public bool LoadAssembly(Assembly assembly)
        {
            return VoltNative.volt_virtual_machine_load_assembly(handle, assembly.Handle);
        }

        /// <summary>
        /// Resets the virtual machine. This will unload any loaded assembly and clear the registers and the stack.
        /// </summary>
        /// <returns>True on success, false on failure.</returns>
        public bool Reset()
        {
            return VoltNative.volt_virtual_machine_reset(handle);
        }

        /// <summary>
        /// Gets the offset of the instruction pointer.
        /// </summary>
        /// <returns>The offset of the instruction pointer.</returns>
        public UInt64 GetInstructionPointer()
        {
            return VoltNative.volt_virtual_machine_get_instruction_pointer(handle);
        }

        /// <summary>
        /// Sets the instruction pointer to the given offset. Only use if you know what you are doing.
        /// </summary>
        /// <param name="offset">The offset that the instruction needs to go to</param>
        /// <returns>True on success, false on failure.</returns>
        public bool SetInstructionPointer(UInt64 offset)
        {
            return VoltNative.volt_virtual_machine_set_instruction_pointer(handle, offset);
        }

        /// <summary>
        /// Executes a loaded assembly.
        /// </summary>
        /// <returns>An ExecutionStatus telling whether the execution was successful or not.</returns>
        public ExecutionStatus Run()
        {
            return VoltNative.volt_virtual_machine_run(handle);
        }

        /// <summary>
        /// Jumps to a label and starts executing from there.
        /// </summary>
        /// <param name="labelOffset">The offset of the label</param>
        /// <returns>An ExecutionStatus telling whether the execution was successful or not.</returns>
        public ExecutionStatus Call(UInt64 labelOffset)
        {
            return VoltNative.volt_virtual_machine_call(handle, labelOffset);
        }

        /// <summary>
        /// Registers a function so it can get called from within a loaded assembly.
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="fn_ptr">A pointer to the function</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool RegisterFunction(string name, VoltVMFunction fn_ptr)
        {
            return VoltNative.volt_function_register(name, fn_ptr);
        }

        /// <summary>
        /// Gets a span with bytes that are inside the registers.
        /// </summary>
        /// <returns>A span with bytes</returns>
        public Span<byte> GetRegisters()
        {
            VoltNative.volt_virtual_machine_get_registers(handle, out Span<byte> buffer);
            return buffer;
        }

        /// <summary>
        /// Gets the stack object associated with this virtual machine instance.
        /// </summary>
        /// <returns>A stack object.</returns>
        public Stack GetStack()
        {
            var sp = VoltNative.volt_virtual_machine_get_stack(handle);
            return new Stack(sp);
        }

        /// <summary>
        /// Releases the allocated memory associated with this VirtualMachine instance.
        /// </summary>
        public void Dispose()
        {
            VoltNative.volt_virtual_machine_destroy(handle);
        }
    }
}
