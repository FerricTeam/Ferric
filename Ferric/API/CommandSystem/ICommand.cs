namespace Ferric.API.CommandSystem
{
    /// <summary>
    /// Defines a command.
    /// </summary>
    public interface ICommand
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
        /// Gets a value indicating whether or not the command requires admin permissions.
        /// </summary>
        bool ServerAdmin { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="arg">The arg.</param>
        void Call(ConsoleSystem.Arg arg);
    }
}