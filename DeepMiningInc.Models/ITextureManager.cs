namespace DeepMiningInc.Engine
{
    public interface ITextureManager
    {
        void RegisterForResourceLoading(ResourceLoadingHandler handler);

        Texture GetTextureFromPath(string path);
    }
}
