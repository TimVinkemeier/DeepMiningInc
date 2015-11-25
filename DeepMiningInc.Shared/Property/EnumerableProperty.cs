using System.Collections.Generic;
using System.Linq;
using DeepMiningInc.Effects;

namespace DeepMiningInc.Property
{
    /// <summary>
    /// Represents a property that has elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public class EnumerableProperty<T> : PropertyBase<IEnumerable<T>>
    {
        public override IEnumerable<T> BaseValueEffect => Effects.Where(e => e.Base == EffectBase.BaseValue).SelectMany(e => e.GetEffectValue(Value));

        public override IEnumerable<T> EffectiveValue => Value.Concat(BaseValueEffect).Concat(EffectiveValueEffect);

        public override IEnumerable<T> EffectiveValueEffect
        {
            get
            {
                var baseValueEffect = BaseValueEffect;
                return Effects.Where(e => e.Base == EffectBase.EffectedValue)
                    .SelectMany(e => e.GetEffectValue(baseValueEffect));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableProperty{T}" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initialValue">The initial value.</param>
        public EnumerableProperty(string key, IEnumerable<T> initialValue = null) : base(key, initialValue)
        {
        }
    }
}