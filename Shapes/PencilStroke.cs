using Avalonia;
using Avalonia.Media;

namespace GraphicsEditor.Shapes;

public class PencilStroke : DrawnShape
{
    private readonly List<Point> _points = new();

    public PencilStroke(IPen pen) : base(pen) { }

    public void AddPoint(Point p)
    {
        if (_points.Count == 0 || _points[^1] != p)
            _points.Add(p);
    }

    public override void Draw(DrawingContext context)
    {
        if (_points.Count == 0) return;

        if (_points.Count == 1)
        {
            double r = Pen.Thickness / 2;
            context.DrawEllipse(Pen.Brush, null, _points[0], r, r);
            return;
        }

        var geometry = new StreamGeometry();
        using (var ctx = geometry.Open())
        {
            ctx.BeginFigure(_points[0], false);
            for (int i = 1; i < _points.Count; i++)
                ctx.LineTo(_points[i]);
            ctx.EndFigure(false);
        }
        context.DrawGeometry(null, Pen, geometry);
    }
}
