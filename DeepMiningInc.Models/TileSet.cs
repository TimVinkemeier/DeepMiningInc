using System;
using System.Threading.Tasks;

using Windows.Foundation;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

namespace DeepMiningInc.Engine
{
    public sealed class TileSet
    {
        public Uri Uri { get; private set; }

        public int CutWidth { get; private set; }

        public int CutHeight { get; private set; }

        public int CutPadding { get; private set; }

        public ICanvasImage TileSetImage { get; private set; }

        private TileSet() { }

        public static async Task<TileSet> FromBitmapUriAsync(ICanvasResourceCreator resourceCreator, Uri uri, int cutWidth, int cutHeight)
            => await FromBitmapUriAsync(resourceCreator, uri, cutWidth, cutHeight, 0);

        public static async Task<TileSet> FromBitmapUriAsync(
            ICanvasResourceCreator resourceCreator,
            Uri uri,
            int cutWidth,
            int cutHeight,
            int cutPadding) => new TileSet
            {
                CutWidth = cutWidth,
                CutHeight = cutHeight,
                CutPadding = cutPadding,
                TileSetImage = await CanvasBitmap.LoadAsync(resourceCreator, uri)
            };

        public ITexture GetTextureFromAbsoluteCoordinates(int x, int y) => new TileSetTexture(this, x, y, CutPadding);

        public ITexture GetTextureFromCutCoordinates(int x, int y)
        {
            var xc = x * CutWidth + (x * 2 + 1) * CutPadding;
            var yc = y * CutHeight + (y * 2 + 1) * CutPadding;
            return new TileSetTexture(this, xc, yc, CutPadding);
        }

        private class TileSetTexture : ITexture
        {
            private TileSet _tileSet;

            private int _x;

            private int _y;

            private int _padding;

            public TileSetTexture(TileSet tileSet, int x, int y, int padding)
            {
                _tileSet = tileSet;
                _x = x;
                _y = y;
                _padding = padding;
            }

            // TODO: add support for padding and CanvasSpriteSheet
            public ICanvasImage AsCanvasImage()
            {
                return new AtlasEffect
                {
                    Source = _tileSet.TileSetImage,
                    SourceRectangle = new Rect(_x, _y, _tileSet.CutWidth, _tileSet.CutHeight)
                };
            }
        }
    }
}
