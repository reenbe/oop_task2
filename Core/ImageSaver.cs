using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Media.Imaging;

namespace GraphicsEditor.Core;

public class ImageSaver
{
    public async Task<bool> SaveAsync(TopLevel owner, Bitmap bitmap)
    {
        var file = await owner.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Сохранить изображение",
            SuggestedFileName = "рисунок",
            DefaultExtension = "png",
            ShowOverwritePrompt = true,
            FileTypeChoices = new[] { FilePickerFileTypes.ImagePng }
        });

        if (file is null) return false;

        await using var stream = await file.OpenWriteAsync();
        bitmap.Save(stream);
        return true;
    }
}
