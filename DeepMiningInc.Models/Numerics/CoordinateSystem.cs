namespace DeepMiningInc.Engine.Numerics
{
    using System;
    using System.Numerics;

    public abstract class CoordinateSystem
    {
        public TileCoordinate OriginalTopLeftTileCoordinate { get; set; }

        public Vector2 ViewCenterOffset { get; set; }

        private float _zoomLevel;
        public float ZoomLevel
        {
            get
            {
                return this._zoomLevel;
            }
            set
            {
                this._zoomLevel = Math.Min(this.MaximumZoomLevel, Math.Max(this.MinimumZoomLevel, value));
            }
        }

        public uint TileWidth { get; }

        public uint TileHeight { get; }

        public float EffectiveTileWidth => this.TileWidth * this.ZoomLevel;

        public float EffectiveTileHeight => this.TileHeight * this.ZoomLevel;

        public abstract float MaximumZoomLevel { get; }
        public abstract float MinimumZoomLevel { get; }

        protected CoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate)
        {
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.ZoomLevel = 1.0f;
            this.ViewCenterOffset = new Vector2(0f);
            this.OriginalTopLeftTileCoordinate = originalTopLeftTileCoordinate;
        }

        public Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates)
        {
            return this.TileCoordinatesToScreenCoordinates(tileCoordinates, 0.0f);
        }

        public abstract Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates, float thisTileHeightFactor);

        public abstract TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates);
    }
}
