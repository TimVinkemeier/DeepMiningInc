using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Engine
{
    public abstract class Entity
    {
        public ICollection<ReactiveBehavior> Behaviors { get; }

        public bool IsEnabled { get; set; }

        public bool IsVisible { get; set; }

        protected Entity()
        {
            Behaviors = new ReactiveBehaviorCollection(this);
        }
    }

    public class ReactiveBehaviorCollection : ICollection<ReactiveBehavior>
    {
        private readonly List<ReactiveBehavior> _behaviors;

        private readonly Entity _entity;

        public ReactiveBehaviorCollection(Entity entity)
        {
            _entity = entity;
            _behaviors = new List<ReactiveBehavior>();
        }

        public IEnumerator<ReactiveBehavior> GetEnumerator() => _behaviors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(ReactiveBehavior item)
        {
            if (item.AttachedEntity != null)
            {
                throw new InvalidOperationException("Cannot attach to more than one entity at a time!");
            }

            item.AttachedEntity = _entity;
            _behaviors.Add(item);
        }

        public void Clear()
        {
            _behaviors.ForEach(_ => _.AttachedEntity = null);
            _behaviors.Clear();
        }

        public bool Contains(ReactiveBehavior item)
        {
            return _behaviors.Contains(item);
        }

        public void CopyTo(ReactiveBehavior[] array, int arrayIndex)
        {
            _behaviors.CopyTo(array, arrayIndex);
        }

        public bool Remove(ReactiveBehavior item)
        {
            if (!Contains(item))
            {
                return false;
            }

            item.AttachedEntity = null;
            return _behaviors.Remove(item);
        }

        public int Count
        {
            get
            {
                return _behaviors.Count;
            }
        }

        public bool IsReadOnly { get; } = false;
    }
}
