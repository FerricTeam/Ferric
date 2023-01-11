namespace Ferric.API.Attributes
{
    using System;
    using Ferric.API.EventSystem.Enums;

    /// <summary>
    /// Marks a event handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventAttribute : Attribute
    {
        /// <summary>
        /// The event type to subscribe to.
        /// </summary>
        public readonly EventType EventType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventAttribute"/> class using the specified player event type.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        public EventAttribute(EventType eventType) => EventType = eventType;
    }
}