#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented

namespace Ferric.Patches.Events.Server
{
    using Ferric.API.Wrappers;
    using Harmony;
    using Steamworks;

    /// <summary>
    /// Patches ServerMgr.UpdateServerInformation.
    /// </summary>
    [HarmonyPatch(typeof(ServerMgr), nameof(ServerMgr.UpdateServerInformation))]
    internal static class ServerInformationUpdatedPatch
    {
        public static void Prefix()
        {
            SteamServer.GameTags += ",ferric";
            if (API.Wrappers.Server.IsModded)
                SteamServer.GameTags += ",modded";
        }
    }
}