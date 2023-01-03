using System;

namespace VoltLangNET
{
    public class Assembly : IDisposable
    {
        private AssemblyPointer handle;

        public AssemblyPointer Handle
        {
            get
            {
                return handle;
            }
        }

        /// <summary>
        /// The number of instructions in the assembly.
        /// </summary>
        public UInt64 NumberOfInstructions
        {
            get
            {
                if(!handle.IsValid)
                    return 0;
                return VoltNative.volt_assembly_get_num_instructions(handle);
            }
        }

        /// <summary>
        /// Creates a new Assembly Instance.
        /// </summary>
        public Assembly()
        {
            handle = VoltNative.volt_assembly_create();
        }

        /// <summary>
        /// Gets information about a symbol in the assembly.
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <param name="startOffset">The starting offset in number of bytes</param>
        /// <param name="size">The size of the associated data in number of bytes</param>
        /// <param name="DataType">The type of the data</param>
        /// <returns>True on success, false on failure.</returns>
        public bool GetSymbolInfo(string name, out UInt64 startOffset, out UInt64 size, out DataType DataType)
        {
            return VoltNative.volt_assembly_get_symbol_info(handle, name, out startOffset, out size, out DataType);
        }

        /// <summary>
        /// Gets information about a label in the assembly.
        /// </summary>
        /// <param name="name">The name of the label</param>
        /// <param name="offset">The offset of the label within the assembly</param>
        /// <returns>True on success, false on failure.</returns>
        public bool GetLabelInfo(string name, out UInt64 offset)
        {
            return VoltNative.volt_assembly_get_label_info(handle, name, out offset);
        }

        /// <summary>
        /// Retrieves an array of symbols. Note that this method allocates and deallocates unmanaged memory on each call.
        /// </summary>
        /// <returns>A string array with symbols defined in the assembly.</returns>
        public string[] GetSymbols()
        {
            return VoltNative.volt_assembly_get_symbols(handle);
        }

        /// <summary>
        /// Retrieves an array of labels. Note that this method allocates and deallocates unmanaged memory on each call.
        /// </summary>
        /// <returns>A string array with labels defined in the assembly.</returns>
        public string[] GetLabels()
        {
            return VoltNative.volt_assembly_get_labels(handle);
        }

        /// <summary>
        /// Releases the allocated memory associated with this Assembly instance.
        /// </summary>
        public void Dispose()
        {
            VoltNative.volt_assembly_destroy(handle);
        }
    }
}