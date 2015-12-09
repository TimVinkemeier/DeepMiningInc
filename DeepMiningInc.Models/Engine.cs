using System;
using System.Numerics;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using DeepMiningInc.Engine.Numerics;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace DeepMiningInc.Engine
{
    public sealed class Engine
    {
        public static Engine Current { get; } = new Engine();

        public ITextureManager TextureManager { get; } = new TextureManager();

        public CoordinateSystem CoordinateSystem { get; set; }

        public Game Game { get; }

        internal CanvasAnimatedControl Control { get; private set; }

        internal Subject<EarlyUpdateEventArgs> EarlyUpdateObservable { get; }

        internal Subject<UpdateEventArgs> UpdateObservable { get; }

        internal Subject<Tuple<CanvasAnimatedControl, PointerRoutedEventArgs>> GlobalPointerMoved { get; }

        internal Subject<Tuple<CanvasAnimatedControl, PointerRoutedEventArgs>> GlobalPointerWheelChanged { get; }

        private Vector2 _lastMousePos;

        private Vector2 _lastMouseScrollPos;

        private Engine()
        {
            // TODO remove debug code
            Game = DebugDataGenerator.CreateGame();
            CoordinateSystem = new IsometricCoordinateSystem(128, 64, new TileCoordinate(0));

            EarlyUpdateObservable = new Subject<EarlyUpdateEventArgs>();
            UpdateObservable = new Subject<UpdateEventArgs>();
            GlobalPointerMoved = new Subject<Tuple<CanvasAnimatedControl, PointerRoutedEventArgs>>();
            GlobalPointerWheelChanged = new Subject<Tuple<CanvasAnimatedControl, PointerRoutedEventArgs>>();

            GlobalPointerMoved.Subscribe(MoveMap);
            GlobalPointerWheelChanged.Subscribe(ZoomMap);
        }

        public void Attach(CanvasAnimatedControl control)
        {
            Control = control;
            control.CreateResources += CreateResources;
            control.Update += Update;
            control.Draw += Render;
            control.PointerMoved += (s, a) => GlobalPointerMoved.OnNext(Tuple.Create(s as CanvasAnimatedControl, a));
            control.PointerWheelChanged += (s, a) => GlobalPointerWheelChanged.OnNext(Tuple.Create(s as CanvasAnimatedControl, a));
        }

        private void CreateResources(ICanvasAnimatedControl control, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(control).AsAsyncAction());
        }

        private async Task CreateResourcesAsync(ICanvasAnimatedControl control)
        {
            await (TextureManager as TextureManager)?.LoadResourcesAsync(control);
        }

        private void Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            EarlyUpdateObservable.OnNext(new EarlyUpdateEventArgs(sender, args));
            UpdateObservable.OnNext(new UpdateEventArgs(sender, args));
        }

        private void Render(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            foreach (var tile in Game.ActiveLevel.Map.Layers[0])
            {
                var image = Current.TextureManager.GetTexture(tile.Value.TextureKey).AsCanvasImage();
                var thisTileWidth = (float)image.GetBounds(args.DrawingSession).Width;
                var thisTileHeight = (float)image.GetBounds(args.DrawingSession).Height;
                var thisTileHeightFactor = thisTileHeight / CoordinateSystem.TileHeight;

                var position = CoordinateSystem.TileCoordinatesToScreenCoordinates(tile.Key, thisTileHeightFactor);
                args.DrawingSession.DrawImage(
                    image,
                    new Rect(position.X, position.Y, CoordinateSystem.EffectiveTileWidth, CoordinateSystem.EffectiveTileHeight * thisTileHeightFactor),
                    new Rect(0, 0, thisTileWidth, thisTileHeight));
            }
        }

        private void MoveMap(Tuple<CanvasAnimatedControl, PointerRoutedEventArgs> args)
        {
            var point = args.Item2.GetCurrentPoint(args.Item1);
            var vector = new Vector2((float)point.Position.X, (float)point.Position.Y);
            if (args.Item2.KeyModifiers.HasFlag(VirtualKeyModifiers.Control))
            {
                _lastMousePos = Vector2.Zero;
                Current.CoordinateSystem.ViewCenterOffset += (vector - _lastMouseScrollPos);
                _lastMouseScrollPos = vector;
            }
            else
            {
                _lastMousePos = vector;
                _lastMouseScrollPos = vector;
            }
        }

        private void ZoomMap(Tuple<CanvasAnimatedControl, PointerRoutedEventArgs> args)
        {
            var point = args.Item2.GetCurrentPoint(args.Item1);
            Current.CoordinateSystem.ZoomLevel += point.Properties.MouseWheelDelta / 1000.0f;
        }
    }

    public class UpdateEventArgs
    {
        public UpdateEventArgs(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

        }
    }

    public class EarlyUpdateEventArgs
    {
        public EarlyUpdateEventArgs(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

        }
    }
}
