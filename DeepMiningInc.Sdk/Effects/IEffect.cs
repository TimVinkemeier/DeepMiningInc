using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Effects
{
    /// <summary>
    /// Interface for effects.
    /// </summary>
    /// <typeparam name="T">The type to which the effect applies.</typeparam>
    public interface IEffect<T>
    {
        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        EffectBase Base { get; }

        /// <summary>
        /// Gets the effect value.
        /// </summary>
        /// <param name="baseValue">The base value, depending on the <see cref="Base"/> setting.</param>
        /// <returns>The value of the effect.</returns>
        T GetEffectValue(T baseValue);
    }
}
