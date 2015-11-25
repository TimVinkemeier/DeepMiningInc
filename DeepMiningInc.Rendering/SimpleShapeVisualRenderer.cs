using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Rendering
{
    public class SimpleShapeVisualRenderer : IRenderer<IRenderingVisual>
    {
        public void Render(IRenderingVisual visual)
        {
            throw new NotImplementedException();
        }

        public Task LoadResourcesAsync(ICanvasResourceCreator resourceCreator)
        {
            throw new NotImplementedException();
        }
    }
}