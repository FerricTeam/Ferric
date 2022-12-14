#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1600 // Elements should be documented

namespace Ferric.Patches.Events.Server
{
    using Facepunch;
    using Ferric.API.EventArgs.Server;
    using Ferric.EventHandlers;
    using Harmony;
    using UnityEngine;

    /// <summary>
    /// Patches <see cref="Facepunch.Output.OnMessage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Output), nameof(Output.LogHandler))]
    internal static class OnMessagePatch
    {
        private static readonly string[] IngoredStartwithMessages = new[]
        {
            "Kinematic body only supports Speculative Continuous collision detection",
            "Skipped frame because GfxDevice",
            "Your current multi-scene setup has inconsistent Lighting",
        };

        private static readonly string[] IgnoreContainsMessages = new[]
        {
            "HandleD3DDeviceLost",
            "ResetD3DDevice",
            "dev->Reset",
            "D3Dwindow device not lost anymore",
            "D3D device reset",
            "group < 0xfff",
            "Mesh can not have more than 65000 vert",
            "Trying to add (Layout Rebuilder for)",
            "Coroutine continue failure",
            "No texture data available to upload",
            "Trying to reload asset from disk that is not",
            "Unable to find shaders used for the terrain engine.",
            "Canvas element contains more than 65535 vertices",
            "RectTransform.set_anchorMin",
            "FMOD failed to initialize the output device",
            "Cannot create FMOD::Sound",
            "invalid utf-16 sequence",
            "missing surrogate tail",
            "Failed to create agent because it is not close enough to the Nav",
            "user-provided triangle mesh descriptor is invalid",
            "Releasing render texture that is set as",
        };

        // port this to IL someday.
        public static bool Prefix(ref string log, ref string stacktrace, ref LogType type)
        {
            ServerOnMessageEventArgs args = new ServerOnMessageEventArgs(log, stacktrace, type);
            ServerHandler.OnServerOnMessage(args);

            if (!args.Allowed)
                return false;

            log = args.Message;
            stacktrace = args.Stacktrace;
            type = args.LogType;

            return true;
        }
    }
}