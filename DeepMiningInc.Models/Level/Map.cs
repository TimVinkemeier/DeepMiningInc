using System.Collections.Generic;

namespace DeepMiningInc.Engine.Level
{
    public sealed class Map
    {
        public SortedList<int, MapLayer> Layers { get; }

        public Map()
        {
            Layers = new SortedList<int, MapLayer>();
        }
    }
}
