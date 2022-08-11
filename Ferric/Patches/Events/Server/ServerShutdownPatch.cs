using Harmony;

namespace Ferric.Patches.Events.Server
{
    /// <summary>
    /// Patches ServerMgr.Shutdown.
    /// </summary>
    [HarmonyPatch(typeof(ServerMgr), nameof(ServerMgr.Shutdown))]
    public static class ServerShutdownPatch
    {
        static void Prefix()
        {
            Loader.Shutdown();
        }
    }
}