using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MbrSanitizer.Services;

internal static class ProjectSanitizer
{
    public static void SanitizeProject(string path, string valueShort, string valueLong, bool deleteMbrFile, bool antiDragImages, string customComment)
    {
        ValidateProjectPath(path);

        SanitizeTags(path);
        SanitizeFileContents(path, valueShort, valueLong, antiDragImages);
        SanitizeAssetContents(path, valueShort, valueLong);
        SanitizeDirectoryNames(path, valueShort, valueLong);

        if(deleteMbrFile)
        {
            FilesManager.DeleteMbrFile(path);
        }

        if(!string.IsNullOrWhiteSpace(customComment))
        {
            FilesManager.AttachCustomCommentToFiles(path, customComment);
        }
    }

    private static void ValidateProjectPath(string path)
    {
        if(string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Project path must not be null, empty or whitespace.", nameof(path));
        }

        if(!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The specified project directory does not exist: '{path}'.");
        }
    }

    private static void SanitizeTags(string path)
    {
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

        string[] htmlFiles;
        try
        {
            htmlFiles = Directory.GetFiles(path, "*.html");
        }
        catch(Exception ex)
        {
            throw new IOException($"Failed to enumerate HTML files in '{path}'.", ex);
        }

        foreach(var htmlFile in htmlFiles)
        {
            try
            {
                var fileLines = File.ReadAllLines(htmlFile);

                var cleaned = fileLines
                    .Where(line => !removePatterns.Any(pattern =>
                        line.Contains(pattern, StringComparison.OrdinalIgnoreCase)))
                    .ToArray();

                File.WriteAllLines(htmlFile, cleaned);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[SanitizeTags] Failed to process file '{htmlFile}': {ex.Message}");
            }
        }
    }

    private static void SanitizeFileContents(string path, string valueShort, string valueLong, bool antiDragImages)
    {
        string[] htmlFiles;
        try
        {
            htmlFiles = Directory.GetFiles(path, "*.html", SearchOption.AllDirectories);
        }
        catch(Exception ex)
        {
            throw new IOException($"Failed to enumerate HTML files recursively in '{path}'.", ex);
        }

        foreach(var htmlFile in htmlFiles)
        {
            try
            {
                if(IsInImageDirectory(htmlFile))
                {
                    continue;
                }

                var content = File.ReadAllText(htmlFile);

                var newContent = content
                    .Replace("alt=\"Mobirise Website Builder\"", "alt=\"image\"", StringComparison.OrdinalIgnoreCase)
                    .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                    .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                    .Replace("href=\"https://mobiri.se/\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                    .Replace("href=\"https://mobiri.se\"", "href=\"#\"", StringComparison.OrdinalIgnoreCase)
                    .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

                if(antiDragImages)
                {
                    newContent = ApplyAntiDragToImages(newContent);
                }

                File.WriteAllText(htmlFile, newContent);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[SanitizeFileContents] Failed to process file '{htmlFile}': {ex.Message}");
            }
        }
    }

    private static void SanitizeAssetContents(string path, string valueShort, string valueLong)
    {
        var assetPath = Path.Combine(path, "assets");

        if(!Directory.Exists(assetPath))
        {
            Debug.WriteLine($"[SanitizeAssetContents] Asset directory does not exist: '{assetPath}'.");
            return;
        }

        string[] textExtensions =
        {
            ".html",
            ".htm",
            ".css",
            ".less",
            ".txt",
            ".js"
        };

        string[] files;
        try
        {
            files = Directory.GetFiles(assetPath, "*.*", SearchOption.AllDirectories);
        }
        catch(Exception ex)
        {
            throw new IOException($"Failed to enumerate files in asset directory '{assetPath}'.", ex);
        }

        foreach(var file in files)
        {
            try
            {
                if(IsInImageDirectory(file))
                {
                    continue;
                }

                var extension = Path.GetExtension(file);
                if(!textExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                var content = File.ReadAllText(file);

                var newContent = content
                    .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                    .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase)
                    .Replace("mobi", valueLong, StringComparison.OrdinalIgnoreCase);

                if(!string.Equals(content, newContent, StringComparison.Ordinal))
                {
                    File.WriteAllText(file, newContent);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[SanitizeAssetContents] Failed to process asset file '{file}': {ex.Message}");
            }
        }
    }

    private static void SanitizeDirectoryNames(string path, string valueShort, string valueLong)
    {
        var assetPath = Path.Combine(path, "assets");

        if(!Directory.Exists(assetPath))
        {
            Debug.WriteLine($"[SanitizeDirectoryNames] Asset directory does not exist: '{assetPath}'.");
            return;
        }

        try
        {
            ProcessDirectory(assetPath, valueShort, valueLong);
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"[SanitizeDirectoryNames] Failed to process asset directory '{assetPath}': {ex.Message}");
        }
    }

    private static void ProcessDirectory(string path, string valueShort, string valueLong)
    {
        // Wie in der alten Klasse: gesamten images-Zweig ignorieren
        if(string.Equals(Path.GetFileName(path), "images", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        foreach(var file in Directory.GetFiles(path))
        {
            try
            {
                var fileName = Path.GetFileName(file);

                var newFileName = fileName
                    .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                    .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

                if(!string.Equals(fileName, newFileName, StringComparison.Ordinal))
                {
                    var newFilePath = Path.Combine(Path.GetDirectoryName(file)!, newFileName);
                    File.Move(file, newFilePath);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[SanitizeDirectoryNames] Failed to rename file '{file}': {ex.Message}");
            }
        }

        foreach(var subDirectory in Directory.GetDirectories(path))
        {
            var currentPath = subDirectory;
            var directoryName = Path.GetFileName(subDirectory);

            var newDirectoryName = directoryName
                .Replace("mbr", valueShort, StringComparison.OrdinalIgnoreCase)
                .Replace("mobirise", valueLong, StringComparison.OrdinalIgnoreCase);

            try
            {
                if(!string.Equals(directoryName, newDirectoryName, StringComparison.Ordinal))
                {
                    var newDirectoryPath = Path.Combine(Path.GetDirectoryName(subDirectory)!, newDirectoryName);
                    Directory.Move(subDirectory, newDirectoryPath);
                    currentPath = newDirectoryPath;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[SanitizeDirectoryNames] Failed to rename directory '{subDirectory}': {ex.Message}");
                currentPath = subDirectory;
            }

            ProcessDirectory(currentPath, valueShort, valueLong);
        }
    }

    private static bool IsInImageDirectory(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if(directoryPath is null)
        {
            return false;
        }

        var directoryName = new DirectoryInfo(directoryPath).Name;
        return directoryName.Equals("images", StringComparison.OrdinalIgnoreCase);
    }

    private static string ApplyAntiDragToImages(string content)
    {
        const string pattern = "<img(?<attrs>[^>]*?)(?<slash>/?)>";

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
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    }
}