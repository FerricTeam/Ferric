namespace Ferric.API.Interfaces
{
    using System;
    using System.Reflection;
    using Ferric.API.Features;

    /// <summary>
    /// Defines basic plugin features.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets or sets the assembly of the plugin.
        /// </summary>
        Assembly Assembly { get; set; }

        /// <summary>
        /// Gets the unique identifier of the plugin.
        /// <remarks>They are not case sensitive.</remarks>
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets the author of the plugin.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the version of the plugin.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets the required Ferric version of the plugin.
        /// </summary>
        Version RequiredFerricVersion { get; }

        /// <summary>
        /// Gets or sets the config class of the plugin.
        /// </summary>
        Config Config { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the plugin will mark the server as <see cref="API.Wrappers.Server.IsModded"/>.
        /// <remarks>https://support.facepunchstudios.com/hc/en-us/articles/360009062817-Guidelines-for-community-servers-using-plugins-mods</remarks>
        /// </summary>
        bool IsModded { get; }

        /// <summary>
        /// Called when the plugin is enabled.
        /// </summary>
        void OnEnabled();

        /// <summary>
        /// Called when the plugin is disabled.
        /// </summary>
        void OnDisabled();
    }
}