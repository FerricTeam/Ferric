namespace Ferric.Injection
{
    using System;
    using System.IO;
    using System.Reflection;
    using UnityEngine;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Injection
    {
        private static bool isLoaded;

        /// <summary>
        /// The entrypoint method.
        /// </summary>
        public static void Start()
        {
            string ferricDir = Path.Combine(
                Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.Parent?.Parent?.FullName ?? string.Empty,
                "Ferric");

            if (isLoaded)
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
                    null);

            isLoaded = true;
        }
    }
}