using System.Text.RegularExpressions;

namespace MbrSanitizer.Services;

internal static class ProjectSanitizerService
{
    public static void SanitizeProject(string path, string valueShort, string valueLong, bool deleteMbrFile, bool antiDragImages, string customComment)
    {
        // Check if the path is valid and exists
        ValidateProjectPath(path);

        // Sanitize the project
        SanitizeTags(path);
        SanitizeFileContents(path, valueShort, valueLong, antiDragImages);
        SanitizeAssetContents(path, valueShort, valueLong);
        SanitizeDirectoryNames(path, valueShort, valueLong);

        // Delete Mobirise-Project file if requested
        if(deleteMbrFile)
            FileManagerService.DeleteMbrFile(path);

        // Attach custom comment to files if provided
        if(!string.IsNullOrWhiteSpace(customComment))
            FileManagerService.AttachCustomCommentToFiles(path, customComment);
    }

    //
    // Local helpers
    //
    private static void ValidateProjectPath(string path)
    {
        // Check if the path is null, empty, or consists only of whitespace
        if(string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Projekt Pfad darf nicht NULL sein, oder aus Leerzeichen bestehen.", nameof(path));

        // Check if the directory exists
        if(!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Das Projekt Verzeichnis existiert nicht: '{path}'.");
    }

    private static void SanitizeTags(string path)
    {
        // Patterns to search for in the HTML files and remove the entire line if found
        string[] removePatterns =
        {
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
            "Mobirise.com </a>"
        };

        // Get all HTML files in the project directory
        var htmlFiles = Directory.GetFiles(path, "*.html");

        // Process each HTML file
        foreach(var htmlFile in htmlFiles)
        {
            // Read file into lines
            var fileLines = File.ReadAllLines(htmlFile);

            // Remove lines that contain any of the specified patterns (case-insensitive)
            var cleaned = fileLines
                .Where(line => !removePatterns.Any(pattern =>
                    line.Contains(pattern, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            // Write the cleaned lines back to the file, only if they have changed
            if(!fileLines.SequenceEqual(cleaned))
                File.WriteAllLines(htmlFile, cleaned);
        }
    }

    private static void SanitizeFileContents(string path, string valueShort, string valueLong, bool antiDragImages)
    {
        // Get all HTML files in the project directory and subdirectories
        var htmlFiles = Directory.GetFiles(path, "*.html", SearchOption.AllDirectories);

        // Process HTML files
        foreach(var htmlFile in htmlFiles)
        {
            // Skip files in the "images" directory
            if(IsInImageDirectory(htmlFile))
                continue;

            // Get the content of the html file
            var content = File.ReadAllText(htmlFile);

            // Replace short and long values, and other specific patterns (links, alt attributes, etc.)
            var newContent = content
                .Replace("alt=\"Mobirise Website Builder\"", "alt=\"image\"", StringComparison.OrdinalIgnoreCase)
                .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                .Replace("href=\"https://mobiri.se/\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                .Replace("href=\"https://mobiri.se\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

            // Add draggable="false" to img tags if requested and not already present
            if(antiDragImages)
                newContent = ApplyAntiDragToImages(newContent);

            // Write the cleaned string back to the file only if it has changed
            if(!string.Equals(content, newContent, StringComparison.Ordinal))
                File.WriteAllText(htmlFile, newContent);
        }
    }

    private static void SanitizeAssetContents(string path, string valueShort, string valueLong)
    {
        // Get the "assets" directory path
        var assetPath = Path.Combine(path, "assets");

        // Check if the assets directory exists
        if(!Directory.Exists(assetPath))
            throw new DirectoryNotFoundException($"Das Projekt Asset-Verzeichnis existiert nicht.");

        // Define the file extensions to process as text files
        string[] textExtensions =
        {
            ".html",
            ".htm",
            ".css",
            ".less",
            ".txt",
            ".js"
        };

        // Get all files in the assets directory and subdirectories
        var files = Directory.GetFiles(assetPath, "*.*", SearchOption.AllDirectories);

        // Process each file
        foreach(var file in files)
        {
            // Check if the file has a text-based extension
            var extension = Path.GetExtension(file);
            if(!textExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                continue;

            // Skip files in the "images" directory
            if(IsInImageDirectory(file))
                continue;

            // Get the content of the file
            var content = File.ReadAllText(file);

            // Replace short and long values
            var newContent = content
                .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

            // Write the cleaned string back to the file only if it has changed
            if(!string.Equals(content, newContent, StringComparison.Ordinal))
                File.WriteAllText(file, newContent);
        }
    }

    private static void SanitizeDirectoryNames(string path, string valueShort, string valueLong)
    {
        // Get the "assets" directory path
        var assetPath = Path.Combine(path, "assets");

        if(!Directory.Exists(assetPath))
            throw new DirectoryNotFoundException($"Das Projekt Asset-Verzeichnis existiert nicht.");

        // Process all directories and their subdirectories and files recursively
        ProcessDirectory(assetPath, valueShort, valueLong);
    }

    private static void ProcessDirectory(string path, string valueShort, string valueLong)
    {
        // Skip renaming if the current directory is "images" to avoid issues with image references
        if(string.Equals(Path.GetFileName(path), "images", StringComparison.OrdinalIgnoreCase))
            return;

        // Process files
        foreach(var file in Directory.GetFiles(path))
        {
            // Get the file name
            var fileName = Path.GetFileName(file);

            // Replace file name
            var newFileName = fileName
                .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

            // Only rename if the new file name is different
            if(!string.Equals(fileName, newFileName, StringComparison.Ordinal))
            {
                var newFilePath = Path.Combine(Path.GetDirectoryName(file)!, newFileName);
                File.Move(file, newFilePath);
            }
        }

        // Process Subdirectories
        foreach(var subDirectory in Directory.GetDirectories(path))
        {
            // Get the path and directory name
            var currentPath = subDirectory;
            var directoryName = Path.GetFileName(subDirectory);

            // Replace directory name
            var newDirectoryName = directoryName
                .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

            // Only rename if the new directory name is different
            if(!string.Equals(directoryName, newDirectoryName, StringComparison.Ordinal))
            {
                var newDirectoryPath = Path.Combine(Path.GetDirectoryName(subDirectory)!, newDirectoryName);
                Directory.Move(subDirectory, newDirectoryPath);
                currentPath = newDirectoryPath;
            }

            // Recursively process
            ProcessDirectory(currentPath, valueShort, valueLong);
        }
    }

    private static bool IsInImageDirectory(string filePath)
    {
        // Get the directory path of the file
        var directoryPath = Path.GetDirectoryName(filePath);

        // Check if the directory path is null
        if(directoryPath is null)
            throw new DirectoryNotFoundException("Das angegebene Verzeichnis ist ungültig.");

        // Check if the directory exists
        if(!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException("Das angegebene Verzeichnis existiert nicht.");

        // Get the name of the directory
        var directoryName = new DirectoryInfo(directoryPath).Name;

        // Return true if the directory name is "images", else -> return false
        return directoryName.Equals("images", StringComparison.OrdinalIgnoreCase);
    }

    private static string ApplyAntiDragToImages(string content)
    {
        // Regular expression pattern to match <img> tags and capture their attributes and optional self-closing slash
        const string pattern = "<img(?<attrs>[^>]*?)(?<slash>/?)>";

        // Use Regex.Replace to find all <img> tags and add draggable="false" if it's not already present in the attributes
        return Regex.Replace(
            content,
            pattern,
            match =>
            {
                var attrs = match.Groups["attrs"].Value;
                var slash = match.Groups["slash"].Value;

                return attrs.IndexOf("draggable=", StringComparison.OrdinalIgnoreCase) >= 0
                    ? match.Value
                    : "<img" + attrs + " draggable=\"false\"" + slash + ">";
            },
            RegexOptions.IgnoreCase);
    }
}