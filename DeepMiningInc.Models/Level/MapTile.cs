namespace DeepMiningInc.Engine.Level
{

    public sealed class MapTile
    {
        public static readonly MapTile Default = new MapTile(string.Empty);

        public string TextureKey { get; }

        public MapTile(string textureKey)
        {
            TextureKey = textureKey;
        }
    }
}
