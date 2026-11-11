using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using GraphicsEditor.Core;

namespace GraphicsEditor;

public partial class MainWindow : Window
{
    private readonly ToolManager _toolManager = new();
    private readonly ImageSaver _imageSaver = new();

    public MainWindow()
    {
        InitializeComponent();

        DrawingCanvas.ToolManager = _toolManager;
        BuildToolButtons();

        ColorSelector.Color = _toolManager.Settings.Color;
        ThicknessSlider.Value = _toolManager.Settings.Thickness;
        ThicknessText.Text = ((int)_toolManager.Settings.Thickness).ToString();

        ColorSelector.ColorChanged += (_, e) => _toolManager.Settings.Color = e.NewColor;
        ThicknessSlider.ValueChanged += (_, e) =>
        {
            _toolManager.Settings.Thickness = e.NewValue;
            ThicknessText.Text = ((int)_toolManager.Settings.Thickness).ToString();
        };
        UndoButton.Click += (_, _) => DrawingCanvas.Undo();
        RedoButton.Click += (_, _) => DrawingCanvas.Redo();
        DrawingCanvas.HistoryChanged += (_, _) => UpdateHistoryButtons();
        UpdateHistoryButtons();
        ClearButton.Click += (_, _) => DrawingCanvas.Clear();
        SaveButton.Click += async (_, _) => await SaveAsync();
    }

    private void UpdateHistoryButtons()
    {
        UndoButton.IsEnabled = DrawingCanvas.CanUndo;
        RedoButton.IsEnabled = DrawingCanvas.CanRedo;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.Key == Key.Z && e.KeyModifiers.HasFlag(KeyModifiers.Meta))
        {
            if (e.KeyModifiers.HasFlag(KeyModifiers.Shift)) DrawingCanvas.Redo();
            else DrawingCanvas.Undo();
            e.Handled = true;
        }
    }

    private void BuildToolButtons()
    {
        foreach (var tool in _toolManager.Tools)
        {
            var button = new RadioButton
            {
                Content = tool.Name,
                GroupName = "Tools",
                IsChecked = tool == _toolManager.ActiveTool,
                Margin = new Thickness(0, 0, 8, 0)
            };
            button.IsCheckedChanged += (_, _) =>
            {
                if (button.IsChecked == true) _toolManager.SelectTool(tool);
            };
            ToolsPanel.Children.Add(button);
        }
    }

    private async Task SaveAsync()
    {
        using var bitmap = DrawingCanvas.RenderToBitmap();
        await _imageSaver.SaveAsync(this, bitmap);
    }
}
