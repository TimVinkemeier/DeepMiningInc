using System;
using System.Numerics;
using System.Threading.Tasks;

using Windows.UI;

using DeepMiningInc.Engine;
using DeepMiningInc.Engine.Numerics;

using Microsoft.Graphics.Canvas;

using Template10.Mvvm;

namespace DeepMiningInc.App.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            ConfigureEngine();
            Engine.Engine.Current.CoordinateSystem = new OrthographicCoordinateSystem(128, 128, new TileCoordinate(0, 0), 0.05f, 3);
            Engine.Engine.Current.TextureManager.RegisterForResourceLoading(LoadResourcesAsync);
        }

        private static void ConfigureEngine()
        {
            Engine.Engine.Current.Configure()
                .WithRendering("ground")
                .WithZoomOnScrollWheel()
                .WithPanOnKeyAndMouseMove();

            Engine.Engine.Current.AddFeature(new TrackFocusedMapTileFeature());

            Engine.Engine.Current.ClearColor = Colors.Black;
        }

        private static async Task LoadResourcesAsync(ITextureRegistrar textureRegistrar, ICanvasResourceCreator resourcecreator, ResourceLoadingArgs args)
        {
            Engine.Engine.Current.CoordinateSystem.ViewCenterOffset = new Vector2((float)args.CanvasSize.Width * 0.5f, (float)args.CanvasSize.Height * 0.5f);

            var bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/stone2d.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("stone2d", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/default2d.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("default", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/wall_we.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("wall_we", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/wall_ne.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("wall_ne", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/wall_nw.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("wall_nw", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/ground.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("ground", SpriteSheet.FromSingleSpriteBitmap(bitmap));
        }
    }
}