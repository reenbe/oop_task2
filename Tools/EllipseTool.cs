using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public class EllipseTool : ShapeTool
{
    public EllipseTool(DrawingSettings settings) : base(settings) { }
    public override string Name => "Эллипс";
    protected override DrawnShape CreateShape(IPen pen, Point start, Point end) => new EllipseShape(pen, start, end);
}
