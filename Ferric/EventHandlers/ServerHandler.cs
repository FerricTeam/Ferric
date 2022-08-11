using System;
using Ferric.API.EventArgs.Server;
using Console = Ferric.API.Wrappers.Console;

namespace Ferric.EventHandlers
{
    public static class ServerHandler
    {
        /// <summary>
        /// Invoked when a <see cref="Console"/> command is sent.
        /// </summary>
        public static event Action<SendingServerCommandEventArgs> SendingServerCommand;
        internal static void OnSendingServerCommand(SendingServerCommandEventArgs args) => SendingServerCommand.InvokeSafely(args);
    }
}