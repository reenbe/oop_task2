using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public abstract class ShapeTool : DrawingTool
{
    private Point _start;
    private Point _current;
    private IPen? _pen;
    private bool _active;

    protected ShapeTool(DrawingSettings settings) : base(settings) { }

    protected abstract DrawnShape CreateShape(IPen pen, Point start, Point end);

    public override void OnMouseDown(Point position)
    {
        _start = _current = position;
        _pen = Settings.CreatePen();
        _active = true;
    }

    public override void OnMouseMove(Point position)
    {
        if (_active) _current = position;
    }

    public override DrawnShape? OnMouseUp(Point position)
    {
        if (!_active || _pen is null) return null;
        var shape = CreateShape(_pen, _start, position);
        Cancel();
        return shape;
    }

    public override void DrawPreview(DrawingContext context)
    {
        if (_active && _pen is not null)
            CreateShape(_pen, _start, _current).Draw(context);
    }

    public override void Cancel()
    {
        _active = false;
        _pen = null;
    }
}
