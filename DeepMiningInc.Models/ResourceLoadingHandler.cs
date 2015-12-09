using System.Threading.Tasks;

using Windows.Foundation;

using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Engine
{
    public delegate Task ResourceLoadingHandler(ITextureManager textureManager, ICanvasResourceCreator resourceCreator, ResourceLoadingArgs args);

    public sealed class ResourceLoadingArgs
    {
        public Size CanvasSize { get; }

        internal ResourceLoadingArgs(Size canvasSize)
        {
            CanvasSize = canvasSize;
        }
    }
}