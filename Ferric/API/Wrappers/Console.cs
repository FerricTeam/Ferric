using System;
using System.Reflection;

namespace Ferric.API.Wrappers
{
    /// <summary>
    /// Used to handle server console-related things.
    /// </summary>
    public static class Console
    {
        public static ServerConsole ConsoleBase => ServerConsole.Instance;

        /// <summary>
        /// Print a info message;
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            ServerConsole.PrintColoured(					
                ConsoleColor.DarkRed,
                "[Ferric]",
                ConsoleColor.DarkCyan,
                "[INFO] ",
                ConsoleColor.DarkCyan,
                message.ToString());
        }
        
        /// <summary>
        /// Print a debug message.
        /// </summary>
        /// <param name="message">The message to output</param>
        /// <param name="print">Whether or not to actually print the message.</param>
        public static void Debug(object message, bool print = true)
        {
            if(!print)
                return;
            ServerConsole.PrintColoured(					
                ConsoleColor.DarkRed,
                "[Ferric]",
                ConsoleColor.Cyan,
                "[DEBUG] ",
                ConsoleColor.Cyan,
                message.ToString());
        }
        
        /// <summary>
        /// Print an warning message.
        /// </summary>
        /// <param name="message">The message to output</param>
        public static void Warn(object message)
        {
            ServerConsole.PrintColoured(					
                ConsoleColor.DarkRed,
                "[Ferric]",
                ConsoleColor.DarkYellow,
                "[WARN] ",
                ConsoleColor.DarkYellow,
                message.ToString());
        }
        
        /// <summary>
        /// Print an error message.
        /// </summary>
        /// <param name="message">The message to output</param>
        public static void Error(object message)
        {
            ServerConsole.PrintColoured(					
                ConsoleColor.DarkRed,
                "[Ferric]",
                ConsoleColor.Red,
                "[ERROR] ",
                ConsoleColor.Red,
                message.ToString());
        }
    }
}