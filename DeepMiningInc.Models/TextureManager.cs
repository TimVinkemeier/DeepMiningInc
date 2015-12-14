using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Numerics;

using Windows.Foundation;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine
{
    internal sealed class TextureManager : ITextureManager, ITextureRegistrar
    {
        private readonly Dictionary<string, SpriteSheet> _sheets;

        private readonly List<ResourceLoadingHandler> _resourceLoadingHandlers;

        public TextureManager()
        {
            _sheets = new Dictionary<string, SpriteSheet>();
            _resourceLoadingHandlers = new List<ResourceLoadingHandler>();
        }

        public void RegisterForResourceLoading(ResourceLoadingHandler handler)
        {
            _resourceLoadingHandlers.Add(handler);
        }

        public Texture GetTextureFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path must not be null!");
            }

            var p = path.Split('/');
            if (p.Length == 1 && !string.IsNullOrWhiteSpace(p[0]))
            {
                return _sheets[p[0]].GetTextureFromIndex(0);
            }

            int index;
            if (p.Length == 2 && !string.IsNullOrWhiteSpace(p[0]) && int.TryParse(p[1], out index))
            {
                return _sheets[p[0]].GetTextureFromIndex(index);
            }

            throw new ArgumentException($"Unrecognized path '{path}'");
        }

        internal async Task LoadResourcesAsync(ICanvasAnimatedControl canvasControl)
        {
            foreach (var handler in _resourceLoadingHandlers)
            {
                await handler?.Invoke(this, canvasControl, new ResourceLoadingArgs(canvasControl?.Size ?? Size.Empty));
            }
        }

        public async Task RegisterSingleTextureAsync(ICanvasResourceCreator resourceCreator, string id, Uri bitmapPath)
        {
            var bitmap = await CanvasBitmap.LoadAsync(resourceCreator, bitmapPath);
            var sheet = SpriteSheet.FromBitmap(bitmap, new Vector2((float)bitmap.Size.Width, (float)bitmap.Size.Height), Vector2.Zero);
            _sheets.Add(id, sheet);
        }

        public async Task RegisterSpriteSheetAsync(ICanvasResourceCreator resourceCreator, string id, Uri bitmapPath, Vector2 spriteSize, Vector2 origin)
        {
            var sheet = await SpriteSheet.FromBitmapUriAsync(resourceCreator, bitmapPath, spriteSize, origin);
            _sheets.Add(id, sheet);
        }

        public Task RegisterSpriteSheetAsync(string id, SpriteSheet spriteSheet)
        {
            _sheets.Add(id, spriteSheet);
            return Task.CompletedTask;
        }
    }
}
