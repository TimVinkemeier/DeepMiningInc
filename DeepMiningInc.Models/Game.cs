using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepMiningInc.Entity;

namespace DeepMiningInc.Models
{
    /// <summary>
    /// Represents a single game.
    /// </summary>
    public class Game
    {
        public IList<IEntity> Entities { get; set; }
    }
}
