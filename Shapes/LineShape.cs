using Avalonia;
using Avalonia.Media;

namespace GraphicsEditor.Shapes;

public class LineShape : DrawnShape
{
    private readonly Point _start, _end;

    public LineShape(IPen pen, Point start, Point end) : base(pen)
    {
        _start = start;
        _end = end;
    }

    public override void Draw(DrawingContext context) => context.DrawLine(Pen, _start, _end);
}
