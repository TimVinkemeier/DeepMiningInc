using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Rendering
{
    /// <summary>
    /// Base interface for renderers.
    /// </summary>
    public interface IRenderer
    {
        Task LoadResourcesAsync(ICanvasResourceCreator resourceCreator);
    }

    /// <summary>
    /// Generic interface for Renderers.
    /// </summary>
    /// <typeparam name="T">The type (or supertype) of the visuals the renderer can render.</typeparam>
    public interface IRenderer<in T> : IRenderer where T : IRenderingVisual
    {
        void Render(T visual);
    }
}