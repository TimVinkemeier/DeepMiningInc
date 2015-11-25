using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Effects
{
    /// <summary>
    /// Interface for classes that can receive effects.
    /// </summary>
    /// <typeparam name="T">The type of the value that can be effected.</typeparam>
    public interface IEffectable<T>
    {
        /// <summary>
        /// Gets the effect that is applied to the base value.
        /// </summary>
        /// <value>
        /// The base value effect.
        /// </value>
        T BaseValueEffect { get; }

        /// <summary>
        /// Gets the effect that is applied to the effective value.
        /// </summary>
        /// <value>
        /// The effective value effect.
        /// </value>
        T EffectiveValueEffect { get; }

        /// <summary>
        /// Gets the effects.
        /// </summary>
        /// <value>
        /// The effects.
        /// </value>
        IList<IEffect<T>> Effects { get; }
    }
}
