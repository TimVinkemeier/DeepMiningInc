using Windows.Foundation;

using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Engine
{

    /// <summary>
    /// Represents a texture.
    /// </summary>
    public struct Texture
    {
        public CanvasBitmap Bitmap { get; set; }

        public Rect SourceRect { get; set; }
    }
}
