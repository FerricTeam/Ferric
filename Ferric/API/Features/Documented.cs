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

        /// <summary>
        /// Initializes a new instance of the <see cref="Documented{T}"/> class.
        /// </summary>
        public Documented()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Documented{T}"/> class via its value and documentation text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="description">The values documentation text.</param>
        public Documented(T value, string description)
        {
            Value = value;
            Description = description;
        }

        /// <inheritdoc />
        public override string ToString() => Value.ToString();
    }
}