namespace DeepMiningInc.Rendering.Numerics
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
                return _zoomLevel;
            }
            set
            {
                _zoomLevel = Math.Min(MaximumZoomLevel, Math.Max(MinimumZoomLevel, value));
            }
        }

        public uint TileWidth { get; }

        public uint TileHeight { get; }

        public float EffectiveTileWidth => TileWidth * ZoomLevel;

        public float EffectiveTileHeight => TileHeight * ZoomLevel;

        public abstract float MaximumZoomLevel { get; }
        public abstract float MinimumZoomLevel { get; }

        protected CoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            ZoomLevel = 1.0f;
            ViewCenterOffset = new Vector2(0f);
            OriginalTopLeftTileCoordinate = originalTopLeftTileCoordinate;
        }

        public Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates)
        {
            return TileCoordinatesToScreenCoordinates(tileCoordinates, 0.0f);
        }

        public abstract Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates, float tileHeight);

        public abstract TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates);
    }
}
