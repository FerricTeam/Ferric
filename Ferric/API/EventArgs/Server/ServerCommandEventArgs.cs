using Ferric.API.EventArgs.Interfaces;
using JetBrains.Annotations;

namespace Ferric.API.EventArgs.Server
{
    /// <summary>
    /// Represents all the information when a console command is sent.
    /// </summary>
    public class SendingServerCommandEventArgs : IEventArg, IDenyable
    {
        /// <summary>
        /// Whether or not the command will be executed.
        /// </summary>
        public bool Allowed { get; set; }
        
        /// <summary>
        /// The <see cref="ConsoleSystem.Arg"/> linked to the command.
        /// </summary>
        public ConsoleSystem.Arg ConsoleSystemArg { get; }
        
        /// <summary>
        /// Gets the <see cref="ConsoleSystem.Command"/> linked to the command.
        /// </summary>
        public ConsoleSystem.Command Command { get; } 

        /// <summary>
        /// Gets or sets the arguments used with the command. Can be null if none are used.
        /// </summary>
        public string[]? Arguments { get; set; }
        
        /// <summary>
        /// Gets whether or not the sender has permission to run this command.
        /// </summary>
        public bool HasPermission { get; }

        public SendingServerCommandEventArgs(ConsoleSystem.Arg args)
        {
            Allowed = true;
            ConsoleSystemArg = args;
            Command = args.cmd;
            Arguments = args.HasArgs() ? args.Args : null;
            HasPermission = args.HasPermission();
        }
    }
}