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