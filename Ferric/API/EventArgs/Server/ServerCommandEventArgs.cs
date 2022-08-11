using Ferric.API.EventArgs.Interfaces;

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

        public SendingServerCommandEventArgs(ConsoleSystem.Arg args)
        {
        }
    }
}