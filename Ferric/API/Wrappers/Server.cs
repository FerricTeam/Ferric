namespace Ferric.API.Wrappers
{
    using System.Collections.Generic;

    /// <summary>
    /// A wrapper class for <see cref="ServerMgr"/>.
    /// </summary>
    public static class Server
    {
        /// <summary>
        /// Gets the ServerMgr instance.
        /// </summary>
        public static ServerMgr Base => ServerMgr.Instance;

        /// <summary>
        /// Gets a value indicating whether or not the server shows up on the modded or on the the community server list.
        /// </summary>
        public static bool IsModded { get; internal set; } = false;

        /// <summary>
        /// Gets or sets the default multiplier of gathering.
        /// </summary>
        public static float DefaultGatheringMultiplier { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the multiplier of gathering for individual items.
        /// Will use <see cref="DefaultGatheringMultiplier"/> if not set.
        /// </summary>
        public static Dictionary<int, float> GatheringMultiplier = new Dictionary<int, float>();

        /// <summary>
        /// Gets or sets the multiplier of gathering for the excavator.
        /// </summary>
        public static float ExcavatorMultiplier
        {
            get => UnityEngine.Object.FindObjectOfType<ExcavatorArm>().resourceProductionTickRate / 3f;
            set
            {
                foreach (var excavatorArm in UnityEngine.Object.FindObjectsOfType<ExcavatorArm>())
                {
                    excavatorArm.resourceProductionTickRate *= 3f * value;
                    excavatorArm.CancelInvoke(nameof(ExcavatorArm.ProduceResources));
                    excavatorArm.InvokeRepeating(nameof(ExcavatorArm.ProduceResources), excavatorArm.resourceProductionTickRate, excavatorArm.resourceProductionTickRate);
                }
            }
        }
    }
}