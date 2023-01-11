namespace Example.Commands
{
    using Ferric.API.Attributes;
    using Ferric.API.CommandSystem;

    /// <inheritdoc />
    [Command(CommandType.Server)]
    public class ExampleCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "example";

        /// <inheritdoc />
        public string Parent { get; } = "exampleplugin";

        /// <inheritdoc />
        public string FullName { get; } = "exampleplugin.example";

        /// <inheritdoc />
        public bool ServerAdmin { get; } = true;

        /// <inheritdoc />
        public void Call(ConsoleSystem.Arg arg)
        {
            arg.Reply = "Hello World!";
        }
    }
}