namespace DeepMiningInc.Engine
{
    public abstract class EngineFeature
    {
        public abstract void Attach(Engine engine);

        public abstract void UnAttach(Engine engine);
    }
}
