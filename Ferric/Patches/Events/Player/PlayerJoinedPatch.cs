namespace Ferric.Patches.Events.Player
{
    using API.EventArgs.Player;
    using API.Wrappers;
    using EventHandlers;
    using Harmony;
    using Network;

    /// <summary>
    /// Patches <see cref="BasePlayer.PlayerInit"/>.
    /// </summary>
    //[HarmonyPatch(typeof(BasePlayer), nameof(BasePlayer.PlayerInit))]
    public static class PlayerJoinedPatch
    {
        public static void Postfix(BasePlayer __instance, Connection c)
        {
            var ply = new Player(__instance);
            Player.List.Add(ply);
            PlayerHandler.OnPlayerJoined(new PlayerJoinedEventArgs(ply));
        }
    }
}