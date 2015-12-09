using System;
using System.Reactive.Linq;

namespace DeepMiningInc.Engine
{
    public abstract class ReactiveBehavior
    {
        protected IObservable<EarlyUpdateEventArgs> EarlyUpdate { get; }

        protected IObservable<UpdateEventArgs> Update { get; }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled && AttachedEntity != null && AttachedEntity.IsEnabled;
            }
            set
            {
                _isEnabled = value;
            }
        }

        public Entity AttachedEntity { get; internal set; }

        protected ReactiveBehavior()
        {
            EarlyUpdate = Engine.Current.EarlyUpdateObservable.DoWhile(() => IsEnabled);
            Update = Engine.Current.UpdateObservable.DoWhile(() => IsEnabled);
        }
    }
}
