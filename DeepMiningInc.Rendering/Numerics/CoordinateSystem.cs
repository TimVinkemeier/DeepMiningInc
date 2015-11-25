namespace DeepMiningInc.Rendering.Numerics
{
    using System.Numerics;

    public abstract class CoordinateSystem
    {
        public TileCoordinate OriginalTopLeftTileCoordinate { get; set; }

        public Vector2 ViewCenterOffset { get; set; }

        public float ZoomLevel { get; set; }

        public uint TileWidth { get; }

        public uint TileHeight { get; }

        public float EffectiveTileWidth => TileWidth * ZoomLevel;

        public float EffectiveTileHeight => TileHeight * ZoomLevel;

        protected CoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            ZoomLevel = 1.0f;
            ViewCenterOffset = new Vector2(0f);
            OriginalTopLeftTileCoordinate = originalTopLeftTileCoordinate;
        }

        public abstract Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates);

        public abstract TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates);
    }
}
