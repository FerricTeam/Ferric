#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Ferric.API.EventArgs.Interfaces
{
    using Ferric.API.Wrappers;

    /// <summary>
    /// Defines an event containing a player.
    /// </summary>
    public interface IPlayerEvent
    {
        public Player Player { get; }
    }
}