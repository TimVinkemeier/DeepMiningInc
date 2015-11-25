using System.Collections.Generic;
using DeepMiningInc.Composition;
using DeepMiningInc.Property;
using DeepMiningInc.Trait;

namespace DeepMiningInc.Entity
{
    /// <summary>
    /// Interface for Entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        ILocation Location { get; }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>
        /// The traits.
        /// </value>
        IReadOnlyProperty<IList<ITrait>> Traits { get; }
    }
}