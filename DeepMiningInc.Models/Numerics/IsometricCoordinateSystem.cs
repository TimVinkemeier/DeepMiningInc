namespace DeepMiningInc.Engine.Numerics
{
    using System.Numerics;

    /// <summary>
    /// Represents an isometric coordinate system.
    /// </summary>
    public class IsometricCoordinateSystem : CoordinateSystem
    {
        public override float MaximumZoomLevel { get; }

        public override float MinimumZoomLevel { get; }

        public IsometricCoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate, float minimumZoomLevel = 0.15f, float maximumZoomLevel = 5.0f)
            : base(tileWidth, tileHeight, originalTopLeftTileCoordinate)
        {
            this.MinimumZoomLevel = minimumZoomLevel;
            this.MaximumZoomLevel = maximumZoomLevel;
            this.ZoomLevel = 1.0f;
        }

        public override Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates, float thisTileHeightFactor)
        {
            var halfTileWidth = this.EffectiveTileWidth / 2;
            var halfTileHeight = this.EffectiveTileHeight / 2;
            var screenX = (tileCoordinates.X - tileCoordinates.Y) * halfTileWidth;
            var screenY = (tileCoordinates.X + tileCoordinates.Y) * halfTileHeight;
            return new Vector2(screenX + this.ViewCenterOffset.X, screenY + this.ViewCenterOffset.Y - (thisTileHeightFactor - 1.0f) * this.EffectiveTileHeight);
        }

        public override TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates)
        {
            var halfTileWidth = this.EffectiveTileWidth / 2;
            var halfTileHeight = this.EffectiveTileHeight / 2;

            var effectiveCoordinate = screenCoordinates - this.ViewCenterOffset;
            effectiveCoordinate -= new Vector2(halfTileWidth, 0);
            effectiveCoordinate = new Vector2(effectiveCoordinate.X / halfTileWidth, effectiveCoordinate.Y / halfTileHeight);
            var tileX = (effectiveCoordinate.X + effectiveCoordinate.Y) / 2;
            var tileY = (effectiveCoordinate.Y - effectiveCoordinate.X) / 2;
            return new TileCoordinate((int)tileX, (int)tileY);
        }
    }
}
