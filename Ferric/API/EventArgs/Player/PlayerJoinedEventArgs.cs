namespace Ferric.API.EventArgs.Player
{
    using Ferric.API.EventArgs.Interfaces;
    using Ferric.API.Wrappers;

    /// <summary>
    /// Represents all the information when a player joins the server.
    /// </summary>
    public class PlayerJoinedEventArgs : IEventArg, IPlayerEvent
    {
        /// <summary>
        /// Gets the <see cref="Player"/> who joined.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class.
        /// </summary>
        /// <param name="ply">Player.</param>
        public PlayerJoinedEventArgs(Player ply) => Player = ply;
    }
}