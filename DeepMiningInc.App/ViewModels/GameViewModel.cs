using System;
using System.Numerics;
using System.Threading.Tasks;

using DeepMiningInc.Engine;

using Microsoft.Graphics.Canvas;

using Template10.Mvvm;

using DeepMiningInc.Engine.Rendering.Texture;

namespace DeepMiningInc.App.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            Engine.Engine.Current.TextureManager.RegisterForResourceLoading(LoadResourcesAsync);
        }

        private async Task LoadResourcesAsync(ITextureManager texturemanager, ICanvasResourceCreator resourcecreator, ResourceLoadingArgs args)
        {
            Engine.Engine.Current.CoordinateSystem.ViewCenterOffset = new Vector2((float)args.CanvasSize.Width * 0.5f, (float)args.CanvasSize.Height * 0.5f);

            var tileSet = await TileSet.FromBitmapUriAsync(resourcecreator, new Uri("ms-appx:///Resources/Textures/stone.png"), 512, 256);
            texturemanager.RegisterTexture("stone", tileSet.GetTextureFromAbsoluteCoordinates(0, 0));

            var defaultTexture = await BitmapTexture.FromUriAsync(resourcecreator, new Uri("ms-appx:///Resources/Textures/default.png"));
            texturemanager.RegisterTexture("default", defaultTexture);
        }
    }
}