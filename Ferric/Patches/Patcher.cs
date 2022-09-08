namespace Ferric.Patches
{
    using Harmony;

    /// <summary>
    /// Handles game patching.
    /// </summary>
    public static class Patcher
    {
        /// <summary>
        /// Gets the Harmony instance.
        /// </summary>
        public static HarmonyInstance Harmony => harmony;
        private static HarmonyInstance harmony;

        /// <summary>
        /// Creates and calls PatchAll() on a new harmony instance.
        /// </summary>
        /// <param name="harmonyId">The Harmony ID.</param>
        internal static void PatchAll(string harmonyId)
        {
            harmony = HarmonyInstance.Create(harmonyId);
            harmony.PatchAll();
        }

        /// <summary>
        /// Removes all harmony patches and nulls the instance.
        /// </summary>
        internal static void UnpatchAll()
        {
            harmony.UnpatchAll(harmony.Id);
            harmony = null;
        }
    }
}