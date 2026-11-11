using Avalonia;
using Avalonia.Media;

namespace GraphicsEditor.Shapes;

public class RectangleShape : DrawnShape
{
    private readonly Rect _rect;

    public RectangleShape(IPen pen, Point a, Point b) : base(pen) => _rect = new Rect(a, b);

    public override void Draw(DrawingContext context) => context.DrawRectangle(null, Pen, _rect);
}
