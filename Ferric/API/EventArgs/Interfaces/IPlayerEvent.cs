namespace Ferric.API.EventArgs.Interfaces
{
    using Wrappers;

    /// <summary>
    /// Defines an event containing a player.
    /// </summary>
    public interface IPlayerEvent
    {
        public Player Player { get; }
    }
}