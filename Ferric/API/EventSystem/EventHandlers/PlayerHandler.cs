namespace Ferric.API.EventSystem.EventHandlers
{
    using System;
    using Ferric.API.Wrappers;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Used to invoke player-related events.
    /// </summary>
    public static class PlayerHandler
    {
        /// <summary>
        /// Invoked when a <see cref="Player"/> joined.
        /// </summary>
        public static Action<Ferric.API.EventSystem.EventArgs.Player.PlayerJoinedEventArgs> PlayerJoined = obj => { };
    }
}