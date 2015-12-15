using System;
using System.Linq;
using System.Numerics;

using Windows.UI.Input;
using Windows.UI.Xaml;

using DeepMiningInc.Engine.Events;
using DeepMiningInc.Engine.Level;

namespace DeepMiningInc.Engine
{
    public sealed class TrackFocusedMapTileFeature : EngineFeature
    {
        private IDisposable _subscription;

        private Vector2 _lastPointerPoint;

        public override void Attach(Engine engine)
        {
            _subscription = engine.GlobalPointerMoved.Subscribe(StoreAndHighlightFocusedMapTile);
        }

        public override void UnAttach(Engine engine)
        {
            _subscription?.Dispose();
        }

        private void StoreAndHighlightFocusedMapTile(PointerMovedCanvasEventArgs args)
        {
            _lastPointerPoint = args.EventArgs.GetCurrentPoint((UIElement)args.Sender).Position.ToVector2();
        }

        public static MapTile GetFocusedMapTile(Engine engine)
        {
            var feature = engine.Features.OfType<TrackFocusedMapTileFeature>().FirstOrDefault();
            if (feature == null)
            {
                throw new InvalidOperationException("The engine does not have this feature added.");
            }

            var coord = engine.CoordinateSystem.ScreenCoordinatesToTileCoordinates(feature._lastPointerPoint);
            return engine.Game.ActiveLevel.Map.Layers[0][coord];
        }
    }
}
