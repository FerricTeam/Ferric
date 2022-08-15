namespace Ferric.API.EventArgs.Player
{
    using Interfaces;
    using Wrappers;

    public class PlayerJoinedEventArgs : IEventArg, IPlayerEvent
    {
        /// <summary>
        /// The <see cref="Player"/> who joined.
        /// </summary>
        public Player Player { get; }
        
        public PlayerJoinedEventArgs(Player ply) => Player = ply;
    }
}