namespace DeepMiningInc.Engine
{
    public interface ITextureManager
    {
        void RegisterForResourceLoading(ResourceLoadingHandler handler);

        void RegisterTexture(string id, ITexture texture);

        ITexture GetTexture(string id);
    }
}
