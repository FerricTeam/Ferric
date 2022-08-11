using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Ferric.Injection
{
    public class Injection
    {
        static bool loaded = false;
        
        public static void Start()
        {
            string ferricDir = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.Parent?.Parent?.FullName,
                "Ferric");
    
            if (loaded)
                return;

            if (!Directory.Exists(ferricDir) || !File.Exists(Path.Combine(ferricDir, "Ferric.dll")))
            {
                ServerConsole.PrintColoured(					
                    ConsoleColor.DarkRed,
                    "[Ferric] ",
                    ConsoleColor.Red,
                    "Couldn't find Ferric file, aborting.");
                return;
            }
            
            ServerConsole.PrintColoured(
                ConsoleColor.DarkRed,
                "[Ferric] ",
                ConsoleColor.Green,
                "Loading Ferric...");

            Assembly.LoadFile(Path.Combine(ferricDir, "Ferric.dll"))
                .GetType("Ferric.Loader")
                .GetMethod("LoadAll")
                ?.Invoke(
                    null,
                    null
                    );

            loaded = true;
        }
    }
}