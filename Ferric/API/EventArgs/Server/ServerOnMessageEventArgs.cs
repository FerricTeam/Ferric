namespace Ferric.API.EventArgs.Server
{
    using Interfaces;
    using JetBrains.Annotations;
    using UnityEngine;

    public class ServerOnMessageEventArgs : IEventArg, IDenyable
    {
        /// <summary>
        /// Gets or sets whether or not the message will be printed to the <see cref="Ferric.API.Wrappers.Console"/>.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets or sets the console message text.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Gets or sets the stacktrace of the message. Can be null.
        /// </summary>
        public string? Stacktrace { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="LogType"/> of the message.
        /// </summary>
        public LogType LogType { get; set; }
        
        public ServerOnMessageEventArgs(string message, string stacktrace, LogType logType)
        {
            Message = message;
            Stacktrace = stacktrace;
            LogType = logType;
            Allowed = true;
        }
    }
}