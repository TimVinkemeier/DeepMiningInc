using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Property
{
    /// <summary>
    /// Interface for variable properties.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IProperty<T> : IReadOnlyProperty<T>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        new T Value { get; set; }

        /// <summary>
        /// Gets the effective value.
        /// </summary>
        /// <value>
        /// The effective value.
        /// </value>
        T EffectiveValue { get; }

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        /// <value>
        /// The initial value.
        /// </value>
        T InitialValue { get; }
    }
}
