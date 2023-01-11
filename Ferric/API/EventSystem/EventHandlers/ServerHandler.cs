namespace Ferric.API.EventSystem.EventHandlers
{
    using System;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Used to invoke server-related events.
    /// </summary>
    public static class ServerHandler
    {
        /// <summary>
        /// Invoked when an <see cref="global::Item"/> is created.
        /// </summary>
        public static Action<Ferric.API.EventSystem.EventArgs.Server.ServerCreatingItemEventArgs> ServerCreatingItem = obj => { };

        /// <summary>
        /// Invoked when a <see cref="Console"/> command is sent.
        /// </summary>
        public static Action<Ferric.API.EventSystem.EventArgs.Server.ServerSendingCommandEventArgs> SendingServerCommand = obj => { };

        /// <summary>
        /// Invoked when a <see cref="Console"/> message is sent.
        /// </summary>
        public static Action<Ferric.API.EventSystem.EventArgs.Server.ServerMessageEventArgs> ServerMessage = obj => { };
    }
}