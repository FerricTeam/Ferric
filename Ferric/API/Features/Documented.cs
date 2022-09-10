namespace Ferric.API.Features
{
    /// <summary>
    /// A documented config value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class Documented<T>
    {
        /// <summary>
        /// The value documentation.
        /// </summary>
        public string Description;

        /// <summary>
        /// The values value.
        /// </summary>
        public T Value;
    }
}