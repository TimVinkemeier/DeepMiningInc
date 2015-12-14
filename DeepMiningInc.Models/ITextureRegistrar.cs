using System;
using System.Numerics;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace DeepMiningInc.Engine
{
    public interface ITextureRegistrar
    {
        Task RegisterSingleTextureAsync(ICanvasResourceCreator resourceCreator, string id, Uri bitmapPath);

        Task RegisterSpriteSheetAsync(ICanvasResourceCreator resourceCreator, string id, Uri bitmapPath, Vector2 spriteSize, Vector2 origin);

        Task RegisterSpriteSheetAsync(string id, SpriteSheet spriteSheet);
    }
}
