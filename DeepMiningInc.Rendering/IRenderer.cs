using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeepMiningInc.Rendering.Numerics;

using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Rendering
{


    public interface IRenderer<T>
    {
        void Render(CanvasDrawingSession drawingSession, CoordinateSystem coordinateSystem);
    }
}
