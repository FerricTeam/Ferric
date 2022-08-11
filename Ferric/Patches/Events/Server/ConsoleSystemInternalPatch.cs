using Ferric.API.EventArgs.Server;
using Ferric.EventHandlers;
using Harmony;
using UnityEngine;

namespace Ferric.Patches.Events.Server
{
    [HarmonyPatch(typeof(ConsoleSystem), nameof(ConsoleSystem.Internal))]
    public static class ConsoleSystemInternalPatch
    {
        public static bool Prefix(ConsoleSystem __instance, ConsoleSystem.Arg arg)
        {
            if (arg is null || arg.Connection is not null && arg.Player() is null)
            {
                return false;
            }
            
            if (arg.cmd.FullName == "chat.say" || arg.cmd.FullName == "chat.teamsay")
            {
                return true;
            }

            var args = new SendingServerCommandEventArgs(arg);
            ServerHandler.OnSendingServerCommand(args);

            if (!args.Allowed)
                return false;

            return true;
        }
    }
}