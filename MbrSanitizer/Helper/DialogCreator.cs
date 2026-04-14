namespace MbrSanitizer.Helper;

internal class DialogCreator
{
    private const string _filter = "JSON files (*.json)|*.json";
    private const string _defaultFileName = "settings.json";

    public static OpenFileDialog CreateOpenFileDialog(string title)
    {
        return new OpenFileDialog()
        {
            FileName = _defaultFileName,
            Filter = _filter,
            Title = title
        };
    }

    public static SaveFileDialog CreateSaveFileDialog(string title)
    {
        return new SaveFileDialog()
        {
            FileName = _defaultFileName,
            Filter = _filter,
            Title = title
        };
    }

    public static FolderBrowserDialog CreateFolderBrowserDialog(string description)
    {
        return new FolderBrowserDialog()
        {
            Description = description,
            UseDescriptionForTitle = true
        };
    }
}