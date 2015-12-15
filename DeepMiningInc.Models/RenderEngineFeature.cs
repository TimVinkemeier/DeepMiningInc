using System;
using System.Numerics;

using DeepMiningInc.Engine.Events;
using DeepMiningInc.Engine.Numerics;

using Windows.Foundation;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

namespace DeepMiningInc.Engine
{
    public sealed class RenderEngineFeature : EngineFeature
    {
        private IDisposable _subscription;

        public string ClearTextureKey { get; set; }

        public override void Attach(Engine engine)
        {
            _subscription = engine.Draw.Subscribe(OnRender);
        }

        public override void UnAttach(Engine engine)
        {
            _subscription?.Dispose();
        }

        private void OnRender(DrawCanvasEventArgs args)
        {
            using (var spriteBatch = args.EventArgs.DrawingSession.CreateSpriteBatch())
            {
                var mouseTile = TrackFocusedMapTileFeature.GetFocusedMapTile(args.Engine);

                if (!string.IsNullOrEmpty(ClearTextureKey))
                {
                    DrawClearTiles(args, spriteBatch);
                }

                var cs = args.Engine.CoordinateSystem;
                foreach (var tile in args.Engine.Game.ActiveLevel.Map.Layers[0])
                {
                    var texture = args.Engine.TextureManager.GetTextureFromPath(tile.Value.TextureKey);
                    var thisTileHeight = (float)texture.SourceRect.Height;
                    var thisTileHeightFactor = thisTileHeight / cs.TileHeight;

                    var position = cs.TileCoordinatesToScreenCoordinates(tile.Key, thisTileHeightFactor);

                    var tint = Vector4.One;

                    if (tile.Value == mouseTile)
                    {
                        tint = new Vector4(1, 3, 1, 1);
                    }

                    spriteBatch.DrawFromSpriteSheet(texture.Bitmap, new Rect(position.X, position.Y, cs.EffectiveTileWidth, cs.EffectiveTileHeight * thisTileHeightFactor), texture.SourceRect, tint);
                }
            }
        }

        private void DrawClearTiles(CanvasEventArgs args, CanvasSpriteBatch spriteBatch)
        {
            var tile = args.Engine.TextureManager.GetTextureFromPath(ClearTextureKey);

            var cs = args.Engine.CoordinateSystem;
            var minTileX = cs.ScreenCoordinatesToTileCoordinates(new Vector2(0, 0)).X - 1;
            var maxTileX = cs.ScreenCoordinatesToTileCoordinates(new Vector2((float)args.Sender.Size.Width, (float)args.Sender.Size.Height)).X + 1;
            var minTileY = cs.ScreenCoordinatesToTileCoordinates(new Vector2((float)args.Sender.Size.Width, 0)).Y - 1;
            var maxTileY = cs.ScreenCoordinatesToTileCoordinates(new Vector2(0, (float)args.Sender.Size.Height)).Y + 1;

            for (var x = minTileX; x <= maxTileX; x++)
            {
                for (var y = minTileY; y <= maxTileY; y++)
                {
                    var thisTileHeight = (float)tile.SourceRect.Height;
                    var thisTileHeightFactor = thisTileHeight / cs.TileHeight;

                    var position = cs.TileCoordinatesToScreenCoordinates(new TileCoordinate(x, y), thisTileHeightFactor);
                    spriteBatch.DrawFromSpriteSheet(
                        tile.Bitmap,
                        new Rect(
                            position.X,
                            position.Y,
                            cs.EffectiveTileWidth,
                            cs.EffectiveTileHeight * thisTileHeightFactor),
                        tile.SourceRect);
                }
            }
        }
    }
}
