namespace Ferric.API.Features
{
    /// <summary>
    /// Represent the configuration file of a <see cref="Plugin"/>.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the plugin is enabled.
        /// </summary>
        public virtual bool Enabled { get; set; }
    }
}