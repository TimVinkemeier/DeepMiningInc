using System;

using Windows.UI.Xaml;

using DeepMiningInc.Engine.Events;

namespace DeepMiningInc.Engine
{
    public class ZoomMapEngineFeature : EngineFeature
    {
        private readonly float _sensitivity;

        private IDisposable _subscription;

        public ZoomMapEngineFeature()
            : this(1.0f)
        { }

        public ZoomMapEngineFeature(float sensitivity)
        {
            _sensitivity = sensitivity;
        }

        public override void Attach(Engine engine)
        {
            _subscription = engine.GlobalPointerWheelChanged.Subscribe(ZoomMap);
        }

        public override void UnAttach(Engine engine)
        {
            _subscription.Dispose();
        }

        private void ZoomMap(PointerWheelChangedCanvasEventArgs args)
        {
            var point = args.EventArgs.GetCurrentPoint(args.Sender as UIElement);
            Engine.Current.CoordinateSystem.ZoomLevel += (point.Properties.MouseWheelDelta / 1000.0f) * _sensitivity;
        }
    }
}
