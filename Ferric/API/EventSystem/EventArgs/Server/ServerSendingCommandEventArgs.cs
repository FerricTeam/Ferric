namespace Ferric.API.EventSystem.EventArgs.Server
{
    using JetBrains.Annotations;

    /// <summary>
    /// Represents all the information when a console command is sent.
    /// </summary>
    public class ServerSendingCommandEventArgs : Interfaces.IEventArg, Interfaces.IDenyable
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the command will be executed.
        /// </summary>
        public bool Allowed { get; set; }

        /// <summary>
        /// Gets the <see cref="ConsoleSystem.Arg"/> linked to the command.
        /// </summary>
        public ConsoleSystem.Arg ConsoleSystemArg { get; }

        /// <summary>
        /// Gets the <see cref="ConsoleSystem.Command"/> linked to the command.
        /// </summary>
        public ConsoleSystem.Command Command { get; }

        /// <summary>
        /// Gets or sets the arguments used with the command. Can be null if none are used.
        /// </summary>
        [CanBeNull]
        public string[] Arguments { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the sender has permission to run this command.
        /// </summary>
        public bool HasPermission { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSendingCommandEventArgs"/> class.
        /// </summary>
        /// <param name="args">ConsoleSystem.Arg.</param>
        public ServerSendingCommandEventArgs(ConsoleSystem.Arg args)
        {
            Allowed = true;
            ConsoleSystemArg = args;
            Command = args.cmd;
            Arguments = args.HasArgs() ? args.Args : null;
            HasPermission = args.HasPermission();
        }
    }
}