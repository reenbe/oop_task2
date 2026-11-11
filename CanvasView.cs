using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using GraphicsEditor.Core;
using GraphicsEditor.Shapes;

namespace GraphicsEditor;

public class CanvasView : Control
{
    private readonly List<DrawnShape> _shapes = new();
    private readonly Stack<DrawnShape> _undone = new();
    private bool _isDrawing;

    public bool CanUndo => _shapes.Count > 0;
    public bool CanRedo => _undone.Count > 0;

    public event EventHandler? HistoryChanged;

    public ToolManager? ToolManager { get; set; }

    public override void Render(DrawingContext context)
    {
        var area = new Rect(Bounds.Size);
        using (context.PushClip(area))
        {
            context.FillRectangle(Brushes.White, area);
            foreach (var shape in _shapes)
                shape.Draw(context);
            ToolManager?.ActiveTool.DrawPreview(context);
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (ToolManager is null || !e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) return;

        _isDrawing = true;
        e.Pointer.Capture(this);
        ToolManager.ActiveTool.OnMouseDown(Clamp(e.GetPosition(this)));
        InvalidateVisual();
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (!_isDrawing || ToolManager is null) return;

        ToolManager.ActiveTool.OnMouseMove(Clamp(e.GetPosition(this)));
        InvalidateVisual();
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (!_isDrawing || ToolManager is null) return;

        _isDrawing = false;
        e.Pointer.Capture(null);

        var shape = ToolManager.ActiveTool.OnMouseUp(Clamp(e.GetPosition(this)));
        if (shape is not null)
        {
            _shapes.Add(shape);
            _undone.Clear();
            HistoryChanged?.Invoke(this, EventArgs.Empty);
        }
        InvalidateVisual();
    }

    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e)
    {
        base.OnPointerCaptureLost(e);
        if (!_isDrawing) return;
        _isDrawing = false;
        ToolManager?.ActiveTool.Cancel();
        InvalidateVisual();
    }

    public void Clear()
    {
        ToolManager?.ActiveTool.Cancel();
        _isDrawing = false;
        _shapes.Clear();
        _undone.Clear();
        HistoryChanged?.Invoke(this, EventArgs.Empty);
        InvalidateVisual();
    }

    public void Undo()
    {
        if (!CanUndo) return;
        _undone.Push(_shapes[^1]);
        _shapes.RemoveAt(_shapes.Count - 1);
        HistoryChanged?.Invoke(this, EventArgs.Empty);
        InvalidateVisual();
    }

    public void Redo()
    {
        if (!CanRedo) return;
        _shapes.Add(_undone.Pop());
        HistoryChanged?.Invoke(this, EventArgs.Empty);
        InvalidateVisual();
    }

    public RenderTargetBitmap RenderToBitmap()
    {
        var size = new PixelSize(Math.Max(1, (int)Bounds.Width), Math.Max(1, (int)Bounds.Height));
        var bitmap = new RenderTargetBitmap(size, new Vector(96, 96));
        bitmap.Render(this);
        return bitmap;
    }

    private Point Clamp(Point p) =>
        new(Math.Clamp(p.X, 0, Bounds.Width), Math.Clamp(p.Y, 0, Bounds.Height));
}
