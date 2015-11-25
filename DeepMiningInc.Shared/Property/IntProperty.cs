using System.Linq;
using DeepMiningInc.Effects;

namespace DeepMiningInc.Property
{
    /// <summary>
    /// Represents a property that has an integer value.
    /// </summary>
    public class IntProperty : PropertyBase<int>
    {
        public override int BaseValueEffect => Effects.Where(e => e.Base == EffectBase.BaseValue).Select(e => e.GetEffectValue(Value)).Sum();

        public override int EffectiveValue => Value + BaseValueEffect + EffectiveValueEffect;

        public override int EffectiveValueEffect
        {
            get
            {
                var baseValueEffect = BaseValueEffect;
                return Effects.Where(e => e.Base == EffectBase.BaseValue).Select(e => e.GetEffectValue(baseValueEffect)).Sum();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntProperty" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initialValue">The initial value.</param>
        public IntProperty(string key, int initialValue = 0) : base(key, initialValue)
        {
        }
    }
}