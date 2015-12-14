using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public sealed class DrawCanvasEventArgs : CanvasEventArgs
    {
        public new CanvasAnimatedDrawEventArgs EventArgs { get; }

        public DrawCanvasEventArgs(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
            : base(sender)
        {
            EventArgs = args;
        }
    }
}
