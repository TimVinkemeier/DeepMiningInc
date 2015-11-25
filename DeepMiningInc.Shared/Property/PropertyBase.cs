using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepMiningInc.Effects;

namespace DeepMiningInc.Property
{
    /// <summary>
    /// Represents a variable property that can receive effects.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public abstract class PropertyBase<T> : IProperty<T>, IEffectable<T>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; }

        /// <summary>
        /// Gets or sets the current base value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; set; }

        /// <summary>
        /// Gets the effective value.
        /// </summary>
        /// <value>
        /// The effective value.
        /// </value>
        public abstract T EffectiveValue { get; }

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        /// <value>
        /// The initial value.
        /// </value>
        public T InitialValue { get; }

        /// <summary>
        /// Gets the effect that is applied to the base value.
        /// </summary>
        /// <value>
        /// The base value effect.
        /// </value>
        public abstract T BaseValueEffect { get; }

        /// <summary>
        /// Gets the effect that is applied to the effective value.
        /// </summary>
        /// <value>
        /// The effective value effect.
        /// </value>
        public abstract T EffectiveValueEffect { get; }

        /// <summary>
        /// Gets the effects.
        /// </summary>
        /// <value>
        /// The effects.
        /// </value>
        public IList<IEffect<T>> Effects { get; } = new List<IEffect<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBase{T}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initialValue">The initial value.</param>
        protected PropertyBase(string key, T initialValue = default(T))
        {
            Key = key;
            Value = initialValue;
            InitialValue = initialValue;
        }
    }
}
