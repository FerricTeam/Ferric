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
        /// The <see cref="ConsoleSystem.Command"/> linked to the command.
        /// </summary>
        public ConsoleSystem.Command Command { get; set; } 

        /// <summary>
        /// The arguments used with the command. Can be null if none are used.
        /// </summary>
        public string[]? Arguments { get; set; }
        
        /// <summary>
        /// Whether or not the sender has permission to run this command.
        /// </summary>
        public bool HasPermission { get; set; }

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