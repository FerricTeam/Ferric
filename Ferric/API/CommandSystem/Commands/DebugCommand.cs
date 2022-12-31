namespace Ferric.API.CommandSystem.Commands
{
    using Ferric.API.Wrappers;

    /// <summary>
    /// Displays debug information.
    /// </summary>
    [Command(CommandType.Server)]
    public class DebugCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "debug";

        /// <inheritdoc />
        public string Parent { get; } = "ferric";

        /// <inheritdoc />
        public string FullName { get; } = "ferric.debug";

        /// <inheritdoc />
        public bool ServerAdmin { get; } = true;

        /// <param name="arg"></param>
        /// <inheritdoc />
        public void Call(ConsoleSystem.Arg arg)
        {
            arg.Reply = "Printing to Console";
            Console.Debug($"Ferric Version: {Loader.Version}");
            Console.Debug($"Is modded: {Server.IsModded}");
            Console.Debug($"Debug variable: {CommandSystem.DebugVariable}");
            Console.Debug("Ferric config: " +
                          $"Config folder: {ConfigManager.FerricConfig.Instance.ConfigsFolder}" +
                          $"Dependencies folder: {ConfigManager.FerricConfig.Instance.DependenciesFolder}" +
                          $"Plugin folder: {ConfigManager.FerricConfig.Instance.PluginFolder}" +
                          $"Ferric folder: {ConfigManager.FerricConfig.FerricFolder}");
            Console.Debug("Plugins: ");
            if (Loader.Plugins.Count != 0)
            {
                foreach (var plugin in Loader.Plugins)
                {
                    Console.Debug($"    {plugin.Name} by {plugin.Author} v{plugin.Version} (ID: {plugin.ID})");
                }
            }
        }
    }
}