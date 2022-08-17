using Ferric.API.Wrappers;
using Harmony;
using Steamworks;

namespace Ferric.Patches.Events.Server
{
    /// <summary>
    /// Patches ServerMgr.UpdateServerInformation.
    /// </summary>
    [HarmonyPatch(typeof(ServerMgr), nameof(ServerMgr.UpdateServerInformation))]
    internal static class ServerInformationUpdatedPatch
    {
        static void Prefix()
        {
            SteamServer.GameTags += ",ferric";
            if(API.Wrappers.Server.IsModded)
                SteamServer.GameTags += ",modded";
        }
    }
}