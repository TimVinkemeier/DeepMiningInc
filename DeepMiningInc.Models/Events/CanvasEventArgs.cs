using System;

using Microsoft.Graphics.Canvas.UI.Xaml;

namespace DeepMiningInc.Engine.Events
{
    public abstract class CanvasEventArgs : EngineEventArgs
    {
        public new ICanvasAnimatedControl Sender { get; }

        public EventArgs EventArgs { get; }

        protected CanvasEventArgs(ICanvasAnimatedControl sender) : base(sender)
        {
            Sender = sender;
        }
    }
}