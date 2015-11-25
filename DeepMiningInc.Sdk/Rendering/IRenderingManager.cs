using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Rendering
{
    public interface IRenderingManager
    {
        IEnumerable<IRenderer> Renderers { get; }
    }
}
