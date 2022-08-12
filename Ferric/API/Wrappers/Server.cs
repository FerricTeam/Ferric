namespace Ferric.API.Wrappers
{
    /// <summary>
    /// Used to handle server-related things.
    /// </summary>
    public static class Server
    {
        /// <summary>
        /// Whether or not the server shows up on the modded or the community server list.
        /// </summary>
        public static bool IsModded { get; internal set; } = false;
    }
}