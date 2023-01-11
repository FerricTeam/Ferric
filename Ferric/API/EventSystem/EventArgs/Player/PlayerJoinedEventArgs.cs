namespace Ferric.API.EventSystem.EventArgs.Player
{
    using Ferric.API.Wrappers;

    /// <summary>
    /// Represents all the information when a player joins the server.
    /// </summary>
    public class PlayerJoinedEventArgs : Ferric.API.EventSystem.EventArgs.Interfaces.IEventArg, Ferric.API.EventSystem.EventArgs.Interfaces.IPlayerEvent
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