// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1400 // Access modifier should be declared
#pragma warning disable SA1306 // Field names should begin with lower-case letter

namespace Ferric.Patcher
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using dnlib.DotNet;
    using dnlib.DotNet.Emit;

    /// <summary>
    /// The main class.
    /// </summary>
    internal class Program
    {
        private const string AssemblyPath = "Assembly-CSharp.dll";
        private const string InjectedAssemblyPath = "Assembly-CSharp-Ferric.dll";
        private const string InjectionPath = "Ferric.Injection.dll";

        private const string BootstrapType = "Bootstrap";
        private const string BootstrapMethod = "StartServer";

        private const string InjectionType = "Ferric.Injection.Injection";
        private const string InjectionMethod = "Start";

        private static ModuleDef ServerAssembly;
        private static ModuleDef InjectionAssembly;

        private static TypeDef BootstrapTypeDef;
        private static MethodDef BootstrapMethodDef;
        private static TypeDef InjectionTypeDef;
        private static MethodDef InjectionMethodDef;

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Loading assemblies...");
                LoadAllAssemblies();

                Console.WriteLine("Locating injection...");
                InjectionTypeDef = FindType(InjectionAssembly, InjectionType);
                if (InjectionTypeDef is null)
                    Error($"Could not find Injection class: {InjectionType}");
                Console.WriteLine($"Found injection: {InjectionTypeDef}");

                Console.WriteLine("Injecting injection...");
                InjectionTypeDef!.Module.Types.Remove(InjectionTypeDef);
                Inject(ServerAssembly, InjectionTypeDef);
                InjectionMethodDef = FindMethod(InjectionTypeDef, InjectionMethod);
                if (InjectionMethodDef is null)
                    Error("Could not locate injected method");
                Console.WriteLine("Injected");

                Console.WriteLine($"Locating {BootstrapType}::{BootstrapMethod}...");
                BootstrapTypeDef = FindType(ServerAssembly, BootstrapType);
                if (BootstrapTypeDef is null)
                    Error($"Cannot locate Bootstrap class ({BootstrapType})");
                BootstrapMethodDef = FindMethod(BootstrapTypeDef, BootstrapMethod);
                if (BootstrapMethodDef is null)
                    Error($"Cannot locate Bootstrap method ({BootstrapMethod})");
                Console.WriteLine($"Located {BootstrapType}::{BootstrapMethod}");

                Console.WriteLine("Patching...");
                BootstrapMethodDef!.Body.Instructions.Insert(0, OpCodes.Call.ToInstruction(InjectionMethodDef));
                Console.WriteLine("Patched");

                Console.WriteLine("Saving...");
                ServerAssembly.Write(InjectedAssemblyPath);
                Console.WriteLine("Saved");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done!");
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
            catch (Exception e)
            {
                Error($"An error occured: {e}");
            }
        }

        private static void Inject(ModuleDef target, TypeDef injection)
        {
            injection.DeclaringType = null;
            target.Types.Add(injection);
        }

        private static void LoadAllAssemblies()
        {
            ServerAssembly = LoadAssembly(AssemblyPath);
            Console.WriteLine($"Loaded {AssemblyPath}");
            InjectionAssembly = LoadAssembly(InjectionPath);
            Console.WriteLine($"Loaded {InjectionPath}");
        }

        private static ModuleDef LoadAssembly(string path)
        {
            Console.WriteLine($"Loading {path}...");
            return ModuleDefMD.Load(path);
        }

        private static MethodDef FindMethod(TypeDef type, string methodName) => type?.Methods.FirstOrDefault(method => method.Name == methodName);

        private static TypeDef FindType(ModuleDef module, string path) => module.Types.Where(x => x is not null && x.IsPublic).FirstOrDefault(type => type.FullName == path);

        private static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.Read();
            Environment.Exit(0);
        }
    }
}