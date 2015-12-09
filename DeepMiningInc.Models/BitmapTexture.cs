using System;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Engine.Rendering.Texture
{
    /// <summary>
    /// Represents a <see cref="Texture"/> consisting of a single bitmap.
    /// </summary>
    public sealed class BitmapTexture : ITexture
    {
        private CanvasBitmap _bitmap;

        private BitmapTexture(CanvasBitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public static async Task<BitmapTexture> FromUriAsync(ICanvasResourceCreator resourceCreator, Uri uri)
        {
            var image = await CanvasBitmap.LoadAsync(resourceCreator, uri);
            return new BitmapTexture(image);
        }

        public ICanvasImage AsCanvasImage()
        {
            return _bitmap;
        }
    }
}
