using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public sealed class EarlyUpdateEngineEventArgs : CanvasEventArgs
    {
        public CanvasAnimatedUpdateEventArgs EventArgs { get; }

        public EarlyUpdateEngineEventArgs(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) : base(sender)
        {
            EventArgs = args;
        }
    }
}