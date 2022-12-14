#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Ferric.Patches.Events.Player
{
    using Ferric.API.EventArgs.Player;
    using Ferric.API.Wrappers;
    using Ferric.EventHandlers;
    using Harmony;
    using Network;

    /// <summary>
    /// Patches <see cref="BasePlayer.PlayerInit"/>.
    /// </summary>
    [HarmonyPatch(typeof(BasePlayer), nameof(BasePlayer.PlayerInit))]
    public static class PlayerJoinedPatch
    {
        public static void Postfix(BasePlayer __instance, Connection c)
        {
            Player ply = new Player(__instance);
            Player.List.Add(ply);
            PlayerHandler.OnPlayerJoined(new PlayerJoinedEventArgs(ply));
        }
    }
}