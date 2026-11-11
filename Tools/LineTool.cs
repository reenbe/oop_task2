using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public class LineTool : ShapeTool
{
    public LineTool(DrawingSettings settings) : base(settings) { }
    public override string Name => "Линия";
    protected override DrawnShape CreateShape(IPen pen, Point start, Point end) => new LineShape(pen, start, end);
}
