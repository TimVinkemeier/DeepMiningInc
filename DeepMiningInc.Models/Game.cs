namespace DeepMiningInc.Engine
{
    using System.Collections.Generic;

    using DeepMiningInc.Entity;

    /// <summary>
    /// Represents a single game.
    /// </summary>
    public class Game
    {
        public IList<Level.Level> Levels { get; }

        public Level.Level ActiveLevel { get; internal set; }

        public Game()
        {
            Levels = new List<Level.Level>();
        }
    }
}
