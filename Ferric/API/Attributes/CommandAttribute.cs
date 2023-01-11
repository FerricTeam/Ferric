namespace Ferric.API.Attributes
{
    using System;
    using Ferric.API.CommandSystem;

    /// <summary>
    /// Marks a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The command type.
        /// </summary>
        public readonly CommandType Type;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class via its type.
        /// </summary>
        /// <param name="type">The command type.</param>
        public CommandAttribute(CommandType type)
        {
            Type = type;
        }

        private CommandAttribute()
        {
        }
    }
}