using Avalonia.Media;

namespace GraphicsEditor.Shapes;

public abstract class DrawnShape
{
    protected DrawnShape(IPen pen) => Pen = pen;

    protected IPen Pen { get; }

    public abstract void Draw(DrawingContext context);
}
