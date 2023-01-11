#pragma warning disable SA1600 // Elements should be documented

namespace Example
{
    using System;
    using Ferric.API.Attributes;
    using Ferric.API.EventSystem.Enums;
    using Ferric.API.EventSystem.EventArgs.Server;
    using Ferric.API.EventSystem.EventHandlers;
    using Console = Ferric.API.Wrappers.Console;

    public class Plugin : Ferric.API.Features.Plugin
    {
        public override string ID { get; } = "FerricExample";
        public override string Author { get; } = "FerricTeam";
        public override string Name { get; } = "Example";
        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredFerricVersion { get; } = new Version(0, 1, 0);

        public override Ferric.API.Features.Config Config { get; set; } = new Config();

        private Config Cfg => Config as Config;

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Console.Debug($"Config int: {Cfg.IntValue}");
            Console.Debug($"Config string: {Cfg.TextValue}");
            Console.Debug($"Config bool: {Cfg.BoolValue}");
            Console.Debug($"Config float: {Cfg.FloatValue}");
            Console.Debug($"Config doc float: {Cfg.DocumentedFloatValue.Description} {Cfg.DocumentedFloatValue.Value}");

            ServerHandler.ServerMessage += ServerOnMessage;

            // Dont do this: this will not be disabled when the plugin is disabled.
            // PlayerHandler.PlayerJoined += args => Console.Debug($"joined: {args.Player.UserId}, {args.Player.IsAdmin}");
            Console.Debug("Enabled!");
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            ServerHandler.ServerMessage -= ServerOnMessage;
            Console.Debug("Disabled!");
        }

        private void ServerOnMessage(ServerMessageEventArgs ev)
        {
            ev.Message += " (modified by plugin)";
        }

        [Event(EventType.SendingServerCommand)]
        private void SendingCommand(ServerSendingCommandEventArgs ev)
        {
            Console.Debug("SendingCommand works!");
            Console.Debug(string.Concat(new object[]
            {
                "Allowed: ",
                ev.Allowed,
                "; Arguments: ",
                string.Join("; ", ev.Arguments ?? new[] { "null" }),
                "; Command details: ",
                ev.Command.Name,
                ev.Command.Parent,
                ev.Command.Description,
                "; Permission: ",
                ev.HasPermission,
                "; Invalid: ",
                ev.ConsoleSystemArg.Invalid,
            }));

            CallDelayed(4f, () => Console.Debug("Delayed message"));
        }
    }
}