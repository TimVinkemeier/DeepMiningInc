namespace DeepMiningInc.Property
{
    /// <summary>
    /// Base class for properties.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IReadOnlyProperty<out T>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        T Value { get; }
    }
}
