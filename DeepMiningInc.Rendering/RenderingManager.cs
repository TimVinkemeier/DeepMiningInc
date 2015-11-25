using System.Collections.Generic;
using System.Threading.Tasks;
using DeepMiningInc.Rendering;
using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Rendering
{
    public class RenderingManager : IRenderingManager
    {
        public IEnumerable<IRenderer> Renderers { get; }

        public RenderingManager()
        {

        }

        public async Task CallLoadResourcesAsync(ICanvasResourceCreator resourceCreator)
        {
            foreach (var renderer in Renderers)
            {
                await renderer.LoadResourcesAsync(resourceCreator);
            }
        }
    }
}
