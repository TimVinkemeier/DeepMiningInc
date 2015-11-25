using DeepMiningInc.Property;

namespace DeepMiningInc.Trait
{
    /// <summary>
    /// Base class for traits with an exposed value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueTrait<T> : ITrait
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public IProperty<T> Value { get; }
    }
}
