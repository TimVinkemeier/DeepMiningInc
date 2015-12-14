using System;
using System.Numerics;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

using Windows.Foundation;

namespace DeepMiningInc.Engine
{
    public sealed class SpriteSheet
    {
        public Uri Uri { get; }

        public Vector2 SpriteSize { get; }

        public Vector2 Origin { get; }

        public CanvasBitmap SpriteImage { get; private set; }

        private readonly int _spritesPerRow;

        private SpriteSheet(Uri uri, CanvasBitmap bitmap, Vector2 spriteSize, Vector2 origin)
        {
            Uri = uri;
            SpriteImage = bitmap;
            SpriteSize = spriteSize;
            Origin = origin;
            _spritesPerRow = (int)(bitmap.Size.Width / spriteSize.X);
        }

        public static SpriteSheet FromSingleSpriteBitmap(CanvasBitmap bitmap)
        {
            return FromBitmap(bitmap, new Vector2((float)bitmap.Size.Width, (float)bitmap.Size.Height), Vector2.Zero);
        }

        public static SpriteSheet FromBitmap(CanvasBitmap bitmap, Vector2 spriteSize, Vector2 origin)
        {
            return new SpriteSheet(null, bitmap, spriteSize, origin);
        }

        public static async Task<SpriteSheet> FromBitmapUriAsync(ICanvasResourceCreator resourceCreator, Uri uri, Vector2 spriteSize, Vector2 origin)
        {
            var bitmap = await CanvasBitmap.LoadAsync(resourceCreator, uri);
            return new SpriteSheet(uri, bitmap, spriteSize, origin);
        }

        public Texture GetTextureFromIndex(int index) => new Texture { Bitmap = SpriteImage, SourceRect = GetSourceRect(index) };

        private Rect GetSourceRect(int sprite)
        {
            var row = sprite / _spritesPerRow;
            var column = sprite % _spritesPerRow;

            return new Rect(SpriteSize.X * column, SpriteSize.Y * row, SpriteSize.X, SpriteSize.Y);
        }
    }
}
