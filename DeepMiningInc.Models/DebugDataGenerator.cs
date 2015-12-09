using System;
using System.Collections.Generic;
using System.Linq;

using DeepMiningInc.Engine.Level;
using DeepMiningInc.Engine.Numerics;

namespace DeepMiningInc.Engine
{
    internal static class DebugDataGenerator
    {
        internal static Game CreateGame()
        {
            var game = new Game();
            var level = CreateLevel();
            game.Levels.Add(level);
            game.ActiveLevel = level;
            return game;
        }


        internal static Level.Level CreateLevel()
        {
            var level = new Level.Level(CreateMap());
            foreach (var e in CreateEntities())
            {
                level.Entities.Add(e);
            }

            return level;
        }

        internal static Map CreateMap()
        {
            var map = new Map();
            map.Layers.Add(0, CreateMapLayer());
            return map;
        }

        internal static MapLayer CreateMapLayer()
        {
            var layer = new MapLayer();
            layer.Index = 0;
            foreach (var t in CreateTiles())
            {
                layer[t.Key] = t.Value;
            }

            return layer;
        }

        internal static IEnumerable<KeyValuePair<TileCoordinate, MapTile>> CreateTiles()
        {
            var SIZE = 15;
            for (var x = 0; x < SIZE; x++)
            {
                for (var y = 0; y < SIZE; y++)
                {
                    yield return new KeyValuePair<TileCoordinate, MapTile>(new TileCoordinate(x, y), new MapTile("default"));
                }
            }
        }

        internal static IEnumerable<Entity> CreateEntities()
        {
            return Enumerable.Empty<Entity>();
        }
    }
}
