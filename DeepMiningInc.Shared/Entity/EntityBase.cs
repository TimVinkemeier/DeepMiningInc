using DeepMiningInc.Composition;
using DeepMiningInc.Property;
using DeepMiningInc.Trait;
using System.Collections.Generic;

namespace DeepMiningInc.Entity
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public ILocation Location { get; }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>
        /// The traits.
        /// </value>
        public IReadOnlyProperty<IList<ITrait>> Traits { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        protected EntityBase(ILocation location)
        {
            Location = location;
        }
    }
}