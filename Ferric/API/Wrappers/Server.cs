namespace Ferric.API.Wrappers
{
    /// <summary>
    /// A wrapper class for <see cref="ServerMgr"/>.
    /// </summary>
    public static class Server
    {
        /// <summary>
        /// Gets the ServerMgr instance.
        /// </summary>
        public static ServerMgr Base => ServerMgr.Instance;

        /// <summary>
        /// Gets a value indicating whether or not the server shows up on the modded or the community server list.
        /// </summary>
        public static bool IsModded { get; internal set; } = false;
    }
}