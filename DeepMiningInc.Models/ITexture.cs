using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Engine
{
    public interface ITexture
    {
        ICanvasImage AsCanvasImage();
    }
}
