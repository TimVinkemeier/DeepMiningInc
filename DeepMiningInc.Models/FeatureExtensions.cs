using Windows.System;

namespace DeepMiningInc.Engine
{
    public static class FeatureExtensions
    {
        public static IEngineConfigurationBuilder Configure(this Engine engine) => new EngineConfigurationBuilder(engine);

        public static IEngineConfigurationBuilder WithZoomOnScrollWheel(
            this IEngineConfigurationBuilder builder,
            float sensitivity = 1.0f)
        {
            builder.Engine.AddFeature(new ZoomMapEngineFeature(sensitivity));
            return builder;
        }

        public static IEngineConfigurationBuilder WithPanOnKeyAndMouseMove(
            this IEngineConfigurationBuilder builder,
            VirtualKeyModifiers modifier = VirtualKeyModifiers.Control)
        {
            builder.Engine.AddFeature(new PanMapEngineFeature(modifier));
            return builder;
        }

        public static IEngineConfigurationBuilder WithRendering(this IEngineConfigurationBuilder builder, string clearTileTextureKey = null)
        {
            builder.Engine.AddFeature(new RenderEngineFeature { ClearTextureKey = clearTileTextureKey });
            return builder;
        }
    }

    public interface IEngineConfigurationBuilder
    {
        Engine Engine { get; }
    }

    internal class EngineConfigurationBuilder : IEngineConfigurationBuilder
    {
        public Engine Engine { get; }

        public EngineConfigurationBuilder(Engine engine)
        {
            Engine = engine;
        }
    }
}
