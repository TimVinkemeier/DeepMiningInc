using System.Collections;
using System.Collections.Generic;

using DeepMiningInc.Engine.Numerics;

namespace DeepMiningInc.Engine.Level
{
    public class MapLayer : IEnumerable<KeyValuePair<TileCoordinate, MapTile>>
    {
        private readonly Dictionary<TileCoordinate, MapTile> _tiles;

        public int Index { get; set; }

        public MapLayer()
        {
            _tiles = new Dictionary<TileCoordinate, MapTile>();
        }

        public MapTile this[TileCoordinate coordinate]
        {
            get
            {
                return _tiles.ContainsKey(coordinate) ? _tiles[coordinate] : MapTile.Default;
            }

            set
            {
                if (_tiles.ContainsKey(coordinate))
                {
                    _tiles[coordinate] = value;
                }
                else
                {
                    _tiles.Add(coordinate, value);
                }
            }
        }

        public IEnumerator<KeyValuePair<TileCoordinate, MapTile>> GetEnumerator() => _tiles.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
