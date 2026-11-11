# Графический редактор (C# / Avalonia, macOS)

Запуск:
    brew install --cask dotnet-sdk   # .NET 8 SDK (если ещё нет)
    cd GraphicsEditor
    dotnet run

Сборка для Mac (Apple Silicon):
    dotnet publish -c Release -r osx-arm64 --self-contained
