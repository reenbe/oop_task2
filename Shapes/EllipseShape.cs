using Avalonia;
using Avalonia.Media;

namespace GraphicsEditor.Shapes;

public class EllipseShape : DrawnShape
{
    private readonly Rect _bounds;

    public EllipseShape(IPen pen, Point a, Point b) : base(pen) => _bounds = new Rect(a, b);

    public override void Draw(DrawingContext context) =>
        context.DrawEllipse(null, Pen, _bounds.Center, _bounds.Width / 2, _bounds.Height / 2);
}
