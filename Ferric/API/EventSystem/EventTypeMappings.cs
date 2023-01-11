#pragma warning disable CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'type'

namespace Ferric.API.EventSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ferric.API.EventSystem.Enums;
    using Ferric.API.EventSystem.EventArgs.Player;
    using Ferric.API.EventSystem.EventArgs.Server;
    using Ferric.API.EventSystem.EventHandlers;

    /// <summary>
    /// Contains mappings from event types to event.
    /// </summary>
    public static class EventTypeMappings
    {
        /// <summary>
        /// Contains the mapping for all server-related event-types to event invokers.
        /// </summary>
        public static readonly Dictionary<EventType, (Type Argument, MulticastDelegate Action)> EventMappings = new()
        {
            [EventType.ServerCreatingItem] = (typeof(ServerCreatingItemEventArgs), ServerHandler.ServerCreatingItem),
            [EventType.SendingServerCommand] = (typeof(ServerSendingCommandEventArgs), ServerHandler.SendingServerCommand),
            [EventType.ServerMessage] = (typeof(ServerMessageEventArgs), ServerHandler.ServerMessage),

            [EventType.PlayerJoined] = (typeof(PlayerJoinedEventArgs), PlayerHandler.PlayerJoined),
        };

        /// <summary>
        /// Gets the mapping for a event type.
        /// </summary>
        /// <param name="key">The event type.</param>
        /// <returns>The mapping. Can be null.</returns>
        public static (Type EventArgs, MulticastDelegate EventAction)? GetMapping(Enum key)
        {
            EventMappings.TryGetValue((EventType)key, out var mapping);
            return mapping;
        }

        /// <summary>
        /// Gets a event type via its <see cref="Action"/>.
        /// </summary>
        /// <param name="multicastDelegate">The <see cref="MulticastDelegate"/>.</param>
        /// <returns>The event type.</returns>
        public static EventType GetEventType(MulticastDelegate multicastDelegate)
        {
            return EventMappings.First(x => x.Value.Action == multicastDelegate).Key;
        }

        /// <summary>
        /// Contains event types and their respective extended subscribers.
        /// </summary>
        public static readonly Dictionary<EventType, (object ClassInstance, MethodInfo[] MethodInfos)[]> ExtendedSubscribersMap = new();
    }
}