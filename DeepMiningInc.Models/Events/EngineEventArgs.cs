namespace DeepMiningInc.Engine.Events
{
    public class EngineEventArgs
    {
        public object Sender { get; }

        public Engine Engine { get; } = Engine.Current;

        public EngineEventArgs(object sender)
        {
            Sender = sender;
        }
    }
}
