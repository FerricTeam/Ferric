#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1602 // Enumeration items should be documented

namespace Ferric.API.EventSystem.Enums
{
    /// <summary>
    /// Ferric events.
    /// </summary>
    public enum EventType
    {
        ServerCreatingItem = 0,
        SendingServerCommand,
        ServerMessage,

        PlayerJoined = 10000,
    }
}