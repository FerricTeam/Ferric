using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Ferric.Patcher
{
    internal class Program
    {
        public static readonly string AssemblyPath = "Assembly-CSharp.dll";
        public static readonly string InjectedAssemblyPath = "Assembly-CSharp-Ferric.dll";
        public static readonly string InjectionPath = "Ferric.Injection.dll";
        
        public static readonly string BootstrapType = "Bootstrap";
        public static readonly string BootstrapMethod= "StartServer";
        public static readonly string InjectionType = "Ferric.Injection.Injection";
        public static readonly string InjectionMethod = "Start";

        static ModuleDef ServerAssembly;
        static ModuleDef InjectionAssembly;

        static TypeDef BootstrapTypeDef;
        static MethodDef BootstrapMethodDef;
        static TypeDef InjectionTypeDef;
        static MethodDef InjectionMethodDef;
        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Loading assemblies...");
                LoadAllAssemblies();
                
                
                Console.WriteLine("Locating injection...");
                InjectionTypeDef = FindType(InjectionAssembly, InjectionType);
                if(InjectionTypeDef is null)
                    Error($"Could not find Injection class: {InjectionType}");
                Console.WriteLine($"Found injection: {InjectionTypeDef}");
                
                
                Console.WriteLine("Injecting injection...");
                InjectionTypeDef.Module.Types.Remove(InjectionTypeDef);
                Inject(ServerAssembly, InjectionTypeDef);
                InjectionMethodDef = FindMethod(InjectionTypeDef, InjectionMethod);
                if(InjectionMethodDef is null)
                    Error("Could not locate injected method");
                Console.WriteLine("Injected");
                
                Console.WriteLine($"Locating {BootstrapType}::{BootstrapMethod}...");
                BootstrapTypeDef = FindType(ServerAssembly, BootstrapType);
                if(BootstrapTypeDef is null)
                    Error($"Cannot locate Bootstrap class ({BootstrapType})");
                BootstrapMethodDef = FindMethod(BootstrapTypeDef, BootstrapMethod);
                if(BootstrapMethodDef is null)
                    Error($"Cannot locate Bootstrap method ({BootstrapMethod})");
                Console.WriteLine($"Located {BootstrapType}::{BootstrapMethod}");
                
                
                Console.WriteLine("Patching...");
                BootstrapMethodDef.Body.Instructions.Insert(0, OpCodes.Call.ToInstruction(InjectionMethodDef));
                Console.WriteLine("Patched");


                Console.WriteLine("Saving...");
                ServerAssembly.Write(InjectedAssemblyPath);
                Console.WriteLine("Saved");

                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done!");
                Thread.Sleep(300);
            }
            catch (Exception e)
            {
                Error($"An error occured: {e}");
            }
        }

        static void Inject(ModuleDef target, TypeDef injection)
        {
            injection.DeclaringType = null;
            target.Types.Add(injection);
        }
        
        static void LoadAllAssemblies()
        {
            ServerAssembly = LoadAssembly(AssemblyPath);
            Console.WriteLine($"Loaded {AssemblyPath}");
            InjectionAssembly = LoadAssembly(InjectionPath);
            Console.WriteLine($"Loaded {InjectionPath}");
        }
        
        static ModuleDef LoadAssembly(string path)
        {
            Console.WriteLine($"Loading {path}...");
            return ModuleDefMD.Load(path);
        }

        static MethodDef FindMethod(TypeDef type, string methodName)
        {
            if (type is not null)
            {
                foreach (var method in type.Methods)
                {
                    if (method.Name == methodName)
                        return method;
                }
            }

            return null;
        }

        static TypeDef FindType(ModuleDef module, string path)
        {
            foreach (var type in module.Types.Where(x => x is not null && x.IsPublic))
            {
                if (type.FullName == path)
                    return type;
            }

            return null;
        }
        
        static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.Read();
            Environment.Exit(0);
        }
    }
}