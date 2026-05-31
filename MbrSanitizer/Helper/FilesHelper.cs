namespace MbrSanitizer.Helper;

internal class FilesHelper
{
    public static string[] GetRemovePatterns(string path)
    {
        // sanitize path
        path = InputSanitizer.SanitizeText(path);

        // check if path exists else -> create file and fill with backup patterns
        if(!File.Exists(path))
        {
            File.WriteAllLines(path, GetBackupPatterns());
        }

        // get patterns from file
        var patterns = File.ReadAllLines(path);

        // if file is empty or null return backup patterns
        if(patterns == null || patterns.Length == 0)
            patterns = GetBackupPatterns();

        return patterns;
    }

    public static string[] GetRemovePatterns()
    {
        // return BackupPatterns
        return GetBackupPatterns();
    }

    //
    // local helpers
    //
    private static string[] GetBackupPatterns()
    {
        // Patterns to search for in the HTML files and remove the entire line if found
        return
        [
            "<meta name=\"generator\"",
            "<!-- Site made with Mobirise Website",
            "AI Website Software</a>",
            "Drag & Drop Website Builder</a>",
            "AI Website Generator</a>",
            "Website Builder Software</a>",
            "Offline Website Builder</a>",
            "Offline Website Maker</a>",
            "Free AI Website Creator</a>",
            "No Code Website Builder</a>",
            "AI Website Builder</a>",
            "Best AI Website Creator</a>",
            "HTML Maker</a>",
            "Mobirise.com</a>",
            "HTML Creator</a>",
            "Website Building Software</a>"
        ];
    }
}