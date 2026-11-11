using Avalonia;
using Avalonia.Media;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Tools;

public abstract class DrawingTool
{
    protected DrawingTool(DrawingSettings settings) => Settings = settings;

    protected DrawingSettings Settings { get; }

    public abstract string Name { get; }

    public abstract void OnMouseDown(Point position);

    public abstract void OnMouseMove(Point position);

    public abstract DrawnShape? OnMouseUp(Point position);

    public abstract void DrawPreview(DrawingContext context);

    public abstract void Cancel();
}
