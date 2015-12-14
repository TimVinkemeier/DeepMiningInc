using Windows.UI.Xaml.Input;

using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public class PointerMovedCanvasEventArgs : CanvasEventArgs
    {
        public PointerRoutedEventArgs EventArgs { get; }

        public PointerMovedCanvasEventArgs(ICanvasAnimatedControl sender, PointerRoutedEventArgs args)
            : base(sender)
        {
            EventArgs = args;
        }
    }
}
