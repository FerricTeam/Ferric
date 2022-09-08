#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented

namespace Ferric.Patches.Events.Server
{
    using Harmony;

    /// <summary>
    /// Patches ServerMgr.Shutdown.
    /// </summary>
    [HarmonyPatch(typeof(ServerMgr), nameof(ServerMgr.Shutdown))]
    internal static class ServerShutdownPatch
    {
        public static void Prefix()
        {
            Loader.Shutdown();
        }
    }
}