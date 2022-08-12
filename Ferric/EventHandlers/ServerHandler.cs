using System;
using Ferric.API.EventArgs.Server;
using Console = Ferric.API.Wrappers.Console;

namespace Ferric.EventHandlers
{
    /// <summary>
    /// Used to invoke server-related events.
    /// </summary>
    public static class ServerHandler
    {
        /// <summary>
        /// Invoked when a <see cref="Console"/> command is sent.
        /// </summary>
        public static event Action<SendingServerCommandEventArgs> SendingServerCommand;

        /// <summary>
        /// Called when a <see cref="Console"/> command is sent.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal static void OnSendingServerCommand(SendingServerCommandEventArgs args)
        {
            try
            {
                SendingServerCommand.InvokeSafely(args);
            }
            catch (Exception e)
            {
                Console.Error(e);
            }
        }
    }
}