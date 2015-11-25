using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMiningInc.Rendering.Numerics
{
    using System.Numerics;

    /// <summary>
    /// Represents an isometric coordinate system.
    /// </summary>
    public class IsometricCoordinateSystem : CoordinateSystem
    {
        private readonly float halfTileWidth;
        private readonly float halfTileHeight;

        public IsometricCoordinateSystem(uint tileWidth, uint tileHeight, TileCoordinate originalTopLeftTileCoordinate)
            : base(tileWidth, tileHeight, originalTopLeftTileCoordinate)
        {
            halfTileWidth = (float)tileWidth / 2;
            halfTileHeight = (float)tileHeight / 2;
        }

        public override Vector2 TileCoordinatesToScreenCoordinates(TileCoordinate tileCoordinates)
        {
            var screenX = (tileCoordinates.X - tileCoordinates.Y) * halfTileWidth;
            var screenY = (tileCoordinates.X + tileCoordinates.Y) * halfTileHeight;
            return new Vector2(screenX + ViewCenterOffset.X, screenY + ViewCenterOffset.Y);
        }

        public override TileCoordinate ScreenCoordinatesToTileCoordinates(Vector2 screenCoordinates)
        {
            var effectiveCoordinate = screenCoordinates - ViewCenterOffset;
            effectiveCoordinate -= new Vector2(halfTileWidth, 0);
            effectiveCoordinate = new Vector2(effectiveCoordinate.X / halfTileWidth, effectiveCoordinate.Y / halfTileHeight);
            var tileX = (effectiveCoordinate.X + effectiveCoordinate.Y) / 2;
            var tileY = (effectiveCoordinate.Y - effectiveCoordinate.X) / 2;
            return new TileCoordinate((int)tileX, (int)tileY);
        }
    }
}
