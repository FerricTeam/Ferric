namespace Ferric.API.Attributes
{
    using System;

    /// <summary>
    /// Marks EventHandler, which contains <see cref="EventAttribute"/>s.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerAttribute"/> class.
        /// </summary>
        public EventHandlerAttribute()
        {
        }
    }
}