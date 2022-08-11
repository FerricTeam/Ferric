using Harmony;

namespace Ferric.Patches
{
    public static class Patcher
    {
        public static HarmonyInstance Harmony => _harmony;
        static HarmonyInstance _harmony;
        
        /// <summary>
        /// Creates and calls PatchAll() on a new harmony instance.
        /// </summary>
        /// <param name="harmonyId">The <see cref="Harmony.Id"/></param>
        internal static void PatchAll(string harmonyId)
        {
            _harmony = HarmonyInstance.Create(harmonyId);
            _harmony.PatchAll();
        }

        /// <summary>
        /// Removes all harmony patches and nulls the instance.
        /// </summary>
        internal static void UnpatchAll()
        {
            _harmony.UnpatchAll(_harmony.Id);
            _harmony = null;
        }
    }
}