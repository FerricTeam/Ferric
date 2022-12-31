namespace Ferric.API.CommandSystem.Commands
{
    using System;

    /// <summary>
    /// Serves no purposed other than testing.
    /// </summary>
    [Command(CommandType.Server)]
    public class DebugVariable : ICommandVariable
    {
        /// <inheritdoc />
        public string Command { get; } = "debugvar";

        /// <inheritdoc />
        public string Parent { get; } = "ferric";

        /// <inheritdoc />
        public string FullName { get; } = "ferric.debugvar";

        /// <inheritdoc />
        public bool ServerAdmin { get; } = true;

        /// <inheritdoc />
        public string Description { get; } = "Debug variable";

        /// <inheritdoc />
        public Func<string> GetOveride { get; } = () => CommandSystem.DebugVariable.ToString();

        /// <inheritdoc />
        public Action<string> SetOveride { get; } = s => { CommandSystem.DebugVariable = s; };
    }
}