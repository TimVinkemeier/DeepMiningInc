using System;
using System.Numerics;

using Windows.System;
using Windows.UI.Xaml;

using DeepMiningInc.Engine.Events;

namespace DeepMiningInc.Engine
{
    public class PanMapEngineFeature : EngineFeature
    {
        private IDisposable _subscription;
        private Vector2 _lastMouseScrollPos;

        private readonly VirtualKeyModifiers _modifier;

        public PanMapEngineFeature(VirtualKeyModifiers modifier = VirtualKeyModifiers.Control)
        {
            _modifier = modifier;
        }

        public override void Attach(Engine engine)
        {
            _subscription = engine.GlobalPointerMoved.Subscribe(PanMap);
        }

        public override void UnAttach(Engine engine)
        {
            _subscription.Dispose();
        }

        private void PanMap(PointerMovedCanvasEventArgs args)
        {
            var point = args.EventArgs.GetCurrentPoint(args.Sender as UIElement);
            var vector = new Vector2((float)point.Position.X, (float)point.Position.Y);
            if (args.EventArgs.KeyModifiers.HasFlag(_modifier))
            {
                Engine.Current.CoordinateSystem.ViewCenterOffset += vector - _lastMouseScrollPos;
                _lastMouseScrollPos = vector;
            }
            else
            {
                _lastMouseScrollPos = vector;
            }
        }
    }
}
