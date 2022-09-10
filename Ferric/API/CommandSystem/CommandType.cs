namespace Ferric.API.CommandSystem
{
    using System;

    /// <summary>
    /// Available command types.
    /// </summary>
    [Flags]
    public enum CommandType
    {
        /// <summary>
        /// Command is executed on the server.
        /// </summary>
        Server,

        /// <summary>
        /// Command is executed on the client.
        /// </summary>
        Client,
    }
}