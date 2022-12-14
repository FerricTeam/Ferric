namespace Ferric.EventHandlers
{
    using System;
    using Ferric.API.EventArgs.Server;
    using Console = Ferric.API.Wrappers.Console;

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
        /// Invoked when a <see cref="Console"/> message is sent.
        /// </summary>
        public static event Action<ServerOnMessageEventArgs> ServerOnMessage;

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

        /// <summary>
        /// Called when a <see cref="Console"/> message is sent.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal static void OnServerOnMessage(ServerOnMessageEventArgs args)
        {
            try
            {
                ServerOnMessage.InvokeSafely(args);
            }
            catch (Exception e)
            {
                Console.Error(e);
            }
        }
    }
}