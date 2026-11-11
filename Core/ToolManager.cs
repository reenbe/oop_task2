using GraphicsEditor.Tools;

namespace GraphicsEditor.Core;

public class ToolManager
{
    private readonly List<DrawingTool> _tools;

    public ToolManager()
    {
        Settings = new DrawingSettings();
        _tools = new List<DrawingTool>
        {
            new PencilTool(Settings),
            new LineTool(Settings),
            new RectangleTool(Settings),
            new EllipseTool(Settings),
        };
        ActiveTool = _tools[0];
    }

    public DrawingSettings Settings { get; }
    public IReadOnlyList<DrawingTool> Tools => _tools;
    public DrawingTool ActiveTool { get; private set; }

    public void SelectTool(DrawingTool tool)
    {
        if (!_tools.Contains(tool) || tool == ActiveTool) return;
        ActiveTool.Cancel();
        ActiveTool = tool;
    }
}
