using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public class UpdateEngineEventArgs : CanvasEventArgs
    {
        public CanvasAnimatedUpdateEventArgs EventArgs { get; }

        public UpdateEngineEventArgs(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args) : base(sender)
        {
            EventArgs = args;
        }
    }
}