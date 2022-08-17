using Harmony;

namespace Ferric.Patches.Events.Server
{
    /// <summary>
    /// Patches ServerMgr.Shutdown.
    /// </summary>
    [HarmonyPatch(typeof(ServerMgr), nameof(ServerMgr.Shutdown))]
    internal static class ServerShutdownPatch
    {
        static void Prefix()
        {
            Loader.Shutdown();
        }
    }
}