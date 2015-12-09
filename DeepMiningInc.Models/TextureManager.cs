using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine
{
    internal sealed class TextureManager : ITextureManager
    {
        private readonly Dictionary<string, ITexture> _textures;

        private readonly List<ResourceLoadingHandler> _resourceLoadingHandlers;

        public TextureManager()
        {
            _textures = new Dictionary<string, ITexture>();
            _resourceLoadingHandlers = new List<ResourceLoadingHandler>();
        }

        public void RegisterForResourceLoading(ResourceLoadingHandler handler)
        {
            _resourceLoadingHandlers.Add(handler);
        }

        public void RegisterTexture(string id, ITexture texture)
        {
            if (_textures.ContainsKey(id))
            {
                throw new InvalidOperationException($"A texture with id '{id}' is already registered.");
            }

            _textures.Add(id, texture);
        }

        public ITexture GetTexture(string id)
        {
            if (!_textures.ContainsKey(id))
            {
                throw new InvalidOperationException($"A texture with id '{id}' does not exist.");
            }

            return _textures[id];
        }

        internal async Task LoadResourcesAsync(ICanvasAnimatedControl canvasControl)
        {
            foreach (var handler in _resourceLoadingHandlers)
            {
                await handler?.Invoke(this, canvasControl, new ResourceLoadingArgs(canvasControl.Size));
            }
        }
    }
}
