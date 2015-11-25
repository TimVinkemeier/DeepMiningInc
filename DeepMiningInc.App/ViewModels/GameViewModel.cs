using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading.Tasks;
using DeepMiningInc.Models;
using DeepMiningInc.Rendering.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;

using Microsoft.Graphics.Canvas.UI.Xaml;
using Template10.Mvvm;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;

using Windows.UI.Xaml.Input;

namespace DeepMiningInc.App.ViewModels
{

    public class GameViewModel : ViewModelBase
    {
        private const float TILE_HEIGHT = 64;
        private const float TILE_WIDTH = 128;
        private CanvasBitmap _test;
        private CanvasBitmap _testBlock;
        private Vector2 _lastMousePos;

        private Vector2 _lastMouseScrollPos;

        private CoordinateSystem _coordinateSystem;
        public ICollection<EntityViewModel> Entities { get; } = new ObservableCollection<EntityViewModel>();
        private Game Game { get; set; }

        public GameViewModel()
        {
            _coordinateSystem = new IsometricCoordinateSystem((uint)TILE_WIDTH, (uint)TILE_HEIGHT, new TileCoordinate(0));
        }

        /// <summary>
        /// Creates the resources.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">  
        /// The <see cref="Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs"/> instance
        /// containing the event data.
        /// </param>
        public void CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            _coordinateSystem.ViewCenterOffset = new Vector2((float)sender.Size.Width * 0.5f, (float)sender.Size.Height * 0.5f);
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
        }

        /// <summary>
        /// Renders the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">  
        /// The <see cref="Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs"/> instance
        /// containing the event data.
        /// </param>
        public void Render(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var mouseTile = _coordinateSystem.ScreenCoordinatesToTileCoordinates(_lastMousePos);

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    ICanvasImage image = _test;
                    var thisTileHeight = (float)image.GetBounds(args.DrawingSession).Height / _coordinateSystem.TileHeight;
                    if (x == mouseTile.X && y == mouseTile.Y && _lastMousePos != Vector2.Zero)
                    {
                        //var effect = new InvertEffect { Source = image };
                        image = _testBlock;
                        thisTileHeight = 1.5f;
                    }
                    var position = _coordinateSystem.TileCoordinatesToScreenCoordinates(new TileCoordinate(x, y), thisTileHeight);
                    args.DrawingSession.DrawImage(
                        image,
                        new Rect(position.X, position.Y, _coordinateSystem.EffectiveTileWidth, _coordinateSystem.EffectiveTileHeight * thisTileHeight), new Rect(0, 0, TILE_WIDTH, TILE_HEIGHT * thisTileHeight));
                }
            }
        }

        public void CanvasControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var cc = sender as CanvasAnimatedControl;
            var point = e.GetCurrentPoint(cc);
            var vector = new Vector2((float)point.Position.X, (float)point.Position.Y);
            if (e.KeyModifiers.HasFlag(VirtualKeyModifiers.Control))
            {
                _lastMousePos = Vector2.Zero;
                _coordinateSystem.ViewCenterOffset += (vector - _lastMouseScrollPos);
                _lastMouseScrollPos = vector;
            }
            else
            {
                _lastMousePos = vector;
                _lastMouseScrollPos = vector;
            }
        }

        public void AnimatedCanvas_PointerWheelChanged(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(sender as UIElement);
            _coordinateSystem.ZoomLevel += point.Properties.MouseWheelDelta / 1000.0f;
        }

        private async Task LoadResourcesAsync(ICanvasAnimatedControl canvasControl)
        {
            var testResource = await CanvasBitmap.LoadAsync(canvasControl, "Resources/Textures/default.png");
            _test = testResource;
            testResource = await CanvasBitmap.LoadAsync(canvasControl, "Resources/Textures/default_block.png");
            _testBlock = testResource;
        }
    }
}