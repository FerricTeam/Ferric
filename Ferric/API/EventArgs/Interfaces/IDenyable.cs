#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Ferric.API.EventArgs.Interfaces
{
    /// <summary>
    /// Defines EventArgs that can be denied.
    /// </summary>
    public interface IDenyable
    {
        public bool Allowed { get; set; }
    }
}