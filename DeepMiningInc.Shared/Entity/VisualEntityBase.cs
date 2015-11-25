using DeepMiningInc.Composition;
using DeepMiningInc.Rendering;

namespace DeepMiningInc.Entity
{
    /// <summary>
    /// Base class for entities that can be rendered.
    /// </summary>
    public abstract class VisualEntityBase : EntityBase, IRenderingVisual
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualEntityBase"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        protected VisualEntityBase(ILocation location) : base(location)
        {
        }
    }
}