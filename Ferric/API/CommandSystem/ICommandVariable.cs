namespace Ferric.API.CommandSystem
{
    using System;

    /// <summary>
    /// Defines a console variable.
    /// </summary>
    [Obsolete("Not implemented", true)]
    public interface ICommandVariable
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        string Command { get; }

        /// <summary>
        /// Gets the command parent.
        /// </summary>
        string Parent { get; }

        /// <summary>
        /// Gets the commands full name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets a value indicating whether or not the command required admin permissions.
        /// </summary>
        bool ServerAdmin { get; }

        /// <summary>
        /// Gets the variables description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a action to get the current value.
        /// </summary>
        Action<string> GetOveride { get; }

        /// <summary>
        /// Gets a action to set a new value.
        /// </summary>
        Action<string> SetOveride { get; }
    }
}