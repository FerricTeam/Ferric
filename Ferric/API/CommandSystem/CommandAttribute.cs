namespace Ferric.API.CommandSystem
{
    using System;

    /// <summary>
    /// Used to mark a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The command type.
        /// </summary>
        public readonly CommandType Type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class via its type.
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