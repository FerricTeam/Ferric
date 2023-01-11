#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable CS1591 // Elements should be documented
#pragma warning disable SA1600 // Elements should be documented

namespace Ferric.Patches.Events.Item
{
    using Ferric.API.EventSystem;
    using Ferric.API.EventSystem.EventArgs.Server;
    using Ferric.API.EventSystem.EventHandlers;
    using Harmony;

    /// <summary>
    /// Patches <see cref="ItemManager.Create"/>.
    /// </summary>
    [HarmonyPatch(typeof(ItemManager), nameof(ItemManager.Create))]
    public class ItemCreatePatch
    {
        public static bool Prefix(ref global::Item __result, ref ItemDefinition template, ref int iAmount, ref ulong skin)
        {
            var ev = new ServerCreatingItemEventArgs(template, iAmount, skin);
            ServerHandler.ServerCreatingItem.InvokeSafely(ev);

            if (!ev.Allowed)
            {
                __result = null;
                return false;
            }

            template = ev.ItemDefinition;
            iAmount = ev.Amount;
            skin = ev.Skin;

            return true;
        }
    }
}