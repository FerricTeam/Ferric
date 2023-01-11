namespace Ferric.API.EventSystem.EventArgs.Server
{
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>
    /// Represents all the information when a console message is sent.
    /// </summary>
    public class ServerMessageEventArgs : Interfaces.IEventArg, Interfaces.IDenyable
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the message will be printed to the <see cref="Ferric.API.Wrappers.Console"/>.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets the console message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the stacktrace of the message. Can be null.
        /// </summary>
        [CanBeNull]
        public string Stacktrace { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LogType"/> of the message.
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="stacktrace">Stacktrace.</param>
        /// <param name="logType">LogType.</param>
        public ServerMessageEventArgs(string message, string stacktrace, LogType logType)
        {
            Message = message;
            Stacktrace = stacktrace;
            LogType = logType;
            Allowed = true;
        }
    }
}