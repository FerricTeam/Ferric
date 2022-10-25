namespace Ferric.API.CommandSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Handles commands.
    /// </summary>
    public static class CommandSystem
    {
        /// <summary>
        /// Initializes the command system.
        /// </summary>
        public static void Init()
        {
            RegisterAssemblyCommands(Assembly.GetExecutingAssembly());

            foreach (var plg in Loader.Plugins.Where(x => x.Config.Enabled))
            {
                RegisterAssemblyCommands(plg.Assembly);
            }

            HandleCache();
        }

        private static List<ConsoleSystem.Command> clientCache = new List<ConsoleSystem.Command>();
        private static List<ConsoleSystem.Command> serverCache = new List<ConsoleSystem.Command>();

        private static void HandleCache()
        {
            bool hasChanged = false;

            if (clientCache.Count != 0)
            {
                hasChanged = true;
                foreach (var cmd in clientCache)
                {
                    ConsoleSystem.Index.Client.Dict.Add(cmd.FullName, cmd);
                    if (cmd.Parent == "global")
                    {
                        ConsoleSystem.Index.Client.GlobalDict.Add(cmd.Name, cmd);
                    }
                }
            }

            if (serverCache.Count != 0)
            {
                hasChanged = true;
                foreach (var cmd in serverCache)
                {
                    ConsoleSystem.Index.Server.Dict.Add(cmd.FullName, cmd);
                    if (cmd.Parent == "global")
                    {
                        ConsoleSystem.Index.Server.GlobalDict.Add(cmd.Name, cmd);
                        Console.Debug("registered");
                    }
                }
            }

            if (hasChanged)
            {
                ConsoleSystem.Index.All = ConsoleSystem.Index.Client.Dict.Values
                    .Union(ConsoleSystem.Index.Server.Dict.Values).ToArray();
            }
        }

        private static void RegisterAssemblyCommands(Assembly assm)
        {
            List<ICommand> commands = new List<ICommand>();

            foreach (var type in assm.GetTypes())
            {
                if (!type.GetInterfaces().Contains(typeof(ICommand)))
                    continue;
                if (type.GetCustomAttributes(typeof(CommandAttribute)).Count() != 0)
                {
                    commands.Add((ICommand)Activator.CreateInstance(type));
                }
            }

            foreach (var cmd in commands)
            {
                foreach (var attr in cmd.GetType().GetCustomAttributes(typeof(CommandAttribute)))
                {
                    if (attr is CommandAttribute cmdAttr)
                    {
                        bool client = cmdAttr.Type.HasFlag(CommandType.Client);
                        var converted = new ConsoleSystem.Command()
                        {
                            Name = cmd.Command,
                            Parent = cmd.Parent,
                            FullName = cmd.FullName,
                            ServerAdmin = cmd.ServerAdmin,
                            Client = client,
                            Variable = false,
                            Call = arg => cmd.Call(arg),
                        };

                        if (client)
                        {
                            clientCache.Add(converted);
                        }

                        if (cmdAttr.Type.HasFlag(CommandType.Server))
                        {
                            serverCache.Add(converted);
                        }
                    }
                }
            }
        }
    }
}