#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable CS1591 // Elements should be documented
#pragma warning disable SA1600 // Elements should be documented

namespace Ferric.Patches.Events.Item
{
    using Ferric.API.EventArgs.Server;
    using Ferric.EventHandlers;
    using Harmony;

    /// <summary>
    /// Patches <see cref="ItemManager.Create"/>.
    /// </summary>
    [HarmonyPatch(typeof(ItemManager), nameof(ItemManager.Create))]
    public class ItemCreatePatch
    {
        public static bool Prefix(ref global::Item __result, ref ItemDefinition __template, ref int __iAmount, ref ulong __skin)
        {
            var ev = new ServerCreatingItemEventArgs(__template, __iAmount, __skin);
            ServerHandler.OnServerCreatingItem(ev);

            if (!ev.Allowed)
            {
                __result = null;
                return false;
            }

            __template = ev.ItemDefinition;
            __iAmount = ev.Amount;
            __skin = ev.Skin;

            return true;
        }
    }
}