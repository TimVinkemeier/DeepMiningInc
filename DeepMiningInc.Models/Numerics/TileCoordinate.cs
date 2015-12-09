namespace DeepMiningInc.Engine.Numerics
{
    using System.Numerics;

    public struct TileCoordinate
    {
        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileCoordinate"/> struct.
        /// </summary>
        /// <param name="xy">The value for the x and y coordinate.</param>
        public TileCoordinate(int xy) : this(xy, xy)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileCoordinate"/> struct.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public TileCoordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Converts this <see cref="TileCoordinate"/> to a corresponding <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Vector2"/>.</returns>
        public Vector2 ToVector2() => new Vector2(this.X, this.Y);

        /// <summary>
        /// Creates a <see cref="TileCoordinate"/> from the given <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector2">The vector2.</param>
        /// <returns>The corresponding <see cref="TileCoordinate"/>.</returns>
        public static TileCoordinate FromVector2(Vector2 vector2) => new TileCoordinate((int)vector2.X, (int)vector2.Y);

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }
    }
}
