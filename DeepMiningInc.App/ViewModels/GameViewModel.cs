using System;
using System.Numerics;
using System.Threading.Tasks;

using Windows.UI;

using DeepMiningInc.Engine;

using Microsoft.Graphics.Canvas;

using Template10.Mvvm;

namespace DeepMiningInc.App.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            ConfigureEngine();
            Engine.Engine.Current.TextureManager.RegisterForResourceLoading(LoadResourcesAsync);
        }

        private static void ConfigureEngine()
        {
            Engine.Engine.Current.Configure()
                .WithRendering("stone")
                .WithZoomOnScrollWheel()
                .WithPanOnKeyAndMouseMove();

            Engine.Engine.Current.ClearColor = Colors.Black;
        }

        private static async Task LoadResourcesAsync(ITextureRegistrar textureRegistrar, ICanvasResourceCreator resourcecreator, ResourceLoadingArgs args)
        {
            Engine.Engine.Current.CoordinateSystem.ViewCenterOffset = new Vector2((float)args.CanvasSize.Width * 0.5f, (float)args.CanvasSize.Height * 0.5f);

            var bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/stone.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("stone", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            bitmap = await
                CanvasBitmap.LoadAsync(
                    resourcecreator,
                    new Uri("ms-appx:///Resources/Textures/default.png", UriKind.Absolute));

            await textureRegistrar.RegisterSpriteSheetAsync("default", SpriteSheet.FromSingleSpriteBitmap(bitmap));

            //await textureRegistrar.RegisterSingleTextureAsync(resourcecreator, "stone", new Uri("ms-appx:///Resources/Textures/stone.png", UriKind.Absolute));
            //await textureRegistrar.RegisterSingleTextureAsync(resourcecreator, "default", new Uri("ms-appx:///Resources/Textures/default.png", UriKind.Absolute));
        }
    }
}