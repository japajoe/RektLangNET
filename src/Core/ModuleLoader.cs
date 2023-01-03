using System;
using System.Collections.Generic;

namespace VoltLangNET
{
    public static class ModuleLoader
    {
        private static Dictionary<IModule, int> modules = new Dictionary<IModule, int>();
        
        public static void Load(IModule module)
        {
            if(modules.ContainsKey(module))
                return;

            modules.Add(module, 1);

            module.Register();
        }

        public static void Dispose()
        {
            foreach(var module in modules.Keys)
            {
                module.Dispose();
            }
        }
    }
}