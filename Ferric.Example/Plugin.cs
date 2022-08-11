using System;
using Ferric.API.EventArgs.Server;
using Ferric.EventHandlers;
using Console = Ferric.API.Wrappers.Console;

namespace Example
{
    public class Plugin : Ferric.API.Features.Plugin
    {
        public override string ID { get; } = "FerricExample";
        public override string Author { get; } = "FerricTeam";
        public override string Name { get; } = "Example";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredFerricVersion { get; } = new Version(0, 1, 0);
        public override Ferric.API.Features.Config Config { get; set; } = new Config();
        
        public Config Cfg => Config as Config;

        public override void OnEnabled()
        {
            Console.Debug("Enabled!");
            Console.Debug($"Config int: {Cfg.intValue}");
            Console.Debug($"Config string: {Cfg.textValue}");
            Console.Debug($"Config bool: {Cfg.boolValue}");
            Console.Debug($"Config float: {Cfg.floatValue}");

            ServerHandler.SendingServerCommand += SendingCommand;
        }

        public override void OnDisabled()
        {
            // implemented (͠≖ ͜ʖ͠≖)
            Console.Debug("Disabled!");
            ServerHandler.SendingServerCommand -= SendingCommand;
        }

        private void SendingCommand(SendingServerCommandEventArgs ev)
        {
            Console.Debug("SendingCommand works!");
        }
    }
}