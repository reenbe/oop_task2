using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public class RectangleTool : ShapeTool
{
    public RectangleTool(DrawingSettings settings) : base(settings) { }
    public override string Name => "Прямоугольник";
    protected override DrawnShape CreateShape(IPen pen, Point start, Point end) => new RectangleShape(pen, start, end);
}
