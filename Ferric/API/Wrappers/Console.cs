namespace Ferric.API.Wrappers
{
    using System;

    /// <summary>
    /// A wrapper class for <see cref="ServerConsole"/>.
    /// </summary>
    public static class Console
    {
        /// <summary>
        /// Gets the ServerConsole instance.
        /// </summary>
        public static ServerConsole Base => ServerConsole.Instance;

        /// <summary>
        /// Print a info message.
        /// </summary>
        /// <param name="message">The message to print.</param>
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
        /// <param name="message">The message to output.</param>
        /// <param name="print">Whether or not to actually print the message.</param>
        public static void Debug(object message, bool print = true)
        {
            if (!print)
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
        /// <param name="message">The message to output.</param>
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
        /// <param name="message">The message to output.</param>
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