using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public class PencilTool : DrawingTool
{
    private PencilStroke? _stroke;

    public PencilTool(DrawingSettings settings) : base(settings) { }

    public override string Name => "Карандаш";

    public override void OnMouseDown(Point position)
    {
        _stroke = new PencilStroke(Settings.CreatePen());
        _stroke.AddPoint(position);
    }

    public override void OnMouseMove(Point position) => _stroke?.AddPoint(position);

    public override DrawnShape? OnMouseUp(Point position)
    {
        if (_stroke is null) return null;
        _stroke.AddPoint(position);
        var result = _stroke;
        _stroke = null;
        return result;
    }

    public override void DrawPreview(DrawingContext context) => _stroke?.Draw(context);

    public override void Cancel() => _stroke = null;
}
