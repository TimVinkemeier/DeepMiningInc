using System;
using System.Reactive.Linq;

using DeepMiningInc.Engine.Events;

namespace DeepMiningInc.Engine
{
    public abstract class ReactiveBehavior
    {
        protected IObservable<EarlyUpdateEngineEventArgs> EarlyUpdate { get; }

        protected IObservable<UpdateEngineEventArgs> Update { get; }

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
            EarlyUpdate = Engine.Current.EarlyUpdateSubject.DoWhile(() => IsEnabled);
            Update = Engine.Current.UpdateSubject.DoWhile(() => IsEnabled);
        }
    }
}
