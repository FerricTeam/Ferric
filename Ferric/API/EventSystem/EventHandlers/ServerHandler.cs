namespace Ferric.API.EventSystem.EventHandlers
{
    using System;
    using Ferric.API.EventSystem.EventArgs.Server;

    /// <summary>
    /// Used to invoke server-related events.
    /// </summary>
    public static class ServerHandler
    {
        /// <summary>
        /// Invoked when an <see cref="global::Item"/> is created.
        /// </summary>
        public static Action<ServerCreatingItemEventArgs> ServerCreatingItem = obj => { };

        /// <summary>
        /// Invoked when a <see cref="Wrappers.Console"/> command is sent.
        /// </summary>
        public static Action<ServerSendingCommandEventArgs> SendingServerCommand = obj => { };

        /// <summary>
        /// Invoked when a <see cref="Wrappers.Console"/> message is sent.
        /// </summary>
        public static Action<ServerMessageEventArgs> ServerMessage = obj => { };
    }
}