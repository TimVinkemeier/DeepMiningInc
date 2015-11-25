using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepMiningInc.Entity;
using Template10.Mvvm;

namespace DeepMiningInc.App.ViewModels
{
    public class EntityViewModel : ViewModelBase
    {
        public IEntity Entity { get; }

        public EntityViewModel(IEntity entity)
        {
            Entity = entity;
        }
    }
}
