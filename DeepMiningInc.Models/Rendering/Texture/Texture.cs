namespace DeepMiningInc.Engine.Rendering.Texture
{
    using Microsoft.Graphics.Canvas;

    /// <summary>
    /// Represents a texture.
    /// </summary>
    public abstract class Texture
    {
        /// <summary>
        /// Gets this instance as an <see cref="ICanvasImage"/>.
        /// </summary>
        /// <returns>This instance as <see cref="ICanvasImage"/>.</returns>
        public abstract ICanvasImage GetAsCanvasImage();
    }
}
