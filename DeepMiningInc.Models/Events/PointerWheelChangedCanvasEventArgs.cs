using Windows.UI.Xaml.Input;

using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public class PointerWheelChangedCanvasEventArgs : CanvasEventArgs
    {
        public PointerRoutedEventArgs EventArgs { get; }

        public PointerWheelChangedCanvasEventArgs(ICanvasAnimatedControl sender, PointerRoutedEventArgs args)
            : base(sender)
        {
            EventArgs = args;
        }
    }
}
