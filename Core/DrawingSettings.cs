using Avalonia.Media;

namespace GraphicsEditor.Core;

public class DrawingSettings
{
    private Color _color = Colors.Black;
    private double _thickness = 3;

    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    public double Thickness
    {
        get => _thickness;
        set => _thickness = Math.Clamp(value, 1, 50);
    }

    public IPen CreatePen() =>
        new Pen(new SolidColorBrush(_color), _thickness, null, PenLineCap.Round, PenLineJoin.Round);
}
