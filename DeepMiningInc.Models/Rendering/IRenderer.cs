namespace DeepMiningInc.Engine.Rendering
{
    using DeepMiningInc.Engine.Numerics;

    using Microsoft.Graphics.Canvas;

    public interface IRenderer<T>
    {
        void Render(CanvasDrawingSession drawingSession, CoordinateSystem coordinateSystem);
    }
}
