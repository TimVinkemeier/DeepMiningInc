using System.Numerics;

namespace DeepMiningInc.Engine.Numerics
{
    public sealed class OrthographicCoordinateSystem : CoordinateSystem
    {
        public OrthographicCoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate, float minimumZoomLevel = 0.15f, float maximumZoomLevel = 5.0f)
            : base(tileWidth, tileHeight, originalTopLeftTileCoordinate)
        {
            MinimumZoomLevel = minimumZoomLevel;
            MaximumZoomLevel = maximumZoomLevel;
            this.ZoomLevel = 1.0f;
        }

        public override float MaximumZoomLevel { get; }

        public override float MinimumZoomLevel { get; }

        public override Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates, float thisTileHeightFactor)
        {
            var screenX = tileCoordinates.X * EffectiveTileWidth;
            var screenY = tileCoordinates.Y * EffectiveTileHeight;
            return new Vector2(screenX + this.ViewCenterOffset.X, screenY + this.ViewCenterOffset.Y - (thisTileHeightFactor - 1.0f) * this.EffectiveTileHeight);
        }

        public override TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates)
        {
            var effectiveCoordinate = screenCoordinates - this.ViewCenterOffset;
            var tile = effectiveCoordinate / EffectiveTileWidth;
            return new TileCoordinate((int)tile.X, (int)tile.Y);
        }
    }
}
