using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeepMiningInc.Entity;

namespace DeepMiningInc.Engine.Level
{
    public sealed class Level
    {
        public Map Map { get; }

        public IList<Entity> Entities { get; }

        public Level(Map map)
        {
            Map = map;
            Entities = new List<Entity>();
        }
    }
}
