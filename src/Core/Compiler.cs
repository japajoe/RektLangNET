using System;

namespace RektLangNET
{
    public class Compiler : IDisposable
    {
        private CompilerPointer handle;

        public CompilerPointer Handle
        {
            get
            {
                return handle;
            }
        }         

        /// <summary>
        /// Creates a new Compiler instance.
        /// </summary>
        public Compiler()
        {
            handle = RektNative.rekt_compiler_create();
        }

        /// <summary>
        /// Compiles an assembly from a filepath.
        /// </summary>
        /// <param name="filepath">The path of the file to compile</param>
        /// <param name="assembly">The assembly instance where to compile to</param>
        /// <returns>True on success, false on failure.</returns>
        public bool CompileFromFile(string filepath, Assembly assembly)
        {
            return RektNative.rekt_compile_from_file(handle, filepath, assembly.Handle);
        }

        /// <summary>
        /// Compiles an assembly from a string.
        /// </summary>
        /// <param name="source">The source code to compile</param>
        /// <param name="assembly">he assembly instance where to compile to</param>
        /// <returns>True on success, false on failure.</returns>
        public bool CompileFromText(string source, Assembly assembly)
        {
            return RektNative.rekt_compile_from_text(handle, source, assembly.Handle);
        }

        /// <summary>
        /// Releases the allocated memory associated with this Compiler instance.
        /// </summary>
        public void Dispose()
        {
            RektNative.rekt_compiler_destroy(handle);
        }
    }
}