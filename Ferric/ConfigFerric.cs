namespace Ferric
{
    using Ferric.API.Features;

    /// <summary>
    /// Contains Ferric configuration variables.
    /// </summary>
    public class ConfigFerric
    {
        /// <summary>
        /// Gets or sets a value whether or not checks for <see cref="Ferric.API.Attributes.EventAttribute"/> usages also look inside private classes.
        /// </summary>
        public Documented<bool> CheckPrivateMethods { get; set; } = new()
        {
            Value = true,
            Description =
                @"Whether or not to check inside private method when registering events.
Only recommended to change from default when told to.",
        };
    }
}