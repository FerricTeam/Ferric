namespace Ferric.EventHandlers
{
    using System;
    using Ferric.API.EventArgs.Player;
    using Ferric.API.Wrappers;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Used to invoke player-related events.
    /// </summary>
    public class PlayerHandler
    {
        /// <summary>
        /// Invoked when a <see cref="Player"/> joined.
        /// </summary>
        public static event Action<PlayerJoinedEventArgs> PlayerJoined;

        /// <summary>
        /// Called when a <see cref="Player"/> joined.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal static void OnPlayerJoined(PlayerJoinedEventArgs args)
        {
            try
            {
                PlayerJoined.InvokeSafely(args);
            }
            catch (Exception e)
            {
                Console.Error(e);
            }
        }
    }
}