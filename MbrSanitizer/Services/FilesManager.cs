using System.Text.RegularExpressions;

namespace MbrSanitizer.Services;

internal class FilesManager
{
    public static void DeleteAllProjectFiles(string path)
    {
        // Normalize the path
        var normalizedPath = Path.GetFullPath(path);

        // Delete all files
        var files = Directory.GetFiles(normalizedPath, "*", SearchOption.AllDirectories);
        foreach(var file in files)
            File.Delete(file);

        // Delete all directories (including childs)
        var directories = Directory.GetDirectories(normalizedPath, "*", SearchOption.AllDirectories);
        // Reverse the array to delete child directories before parent directories
        Array.Reverse(directories);
        foreach(var directory in directories)
            Directory.Delete(directory);
    }

    public static void DeleteMbrFile(string path)
    {
        // Normalize the path
        var normalizedPath = Path.GetFullPath(path);
        var projectFile = Path.Combine(normalizedPath, "project.mobirise");

        // Check if the project.mobirise file exists
        if(!File.Exists(projectFile))
            throw new FileNotFoundException("Im Projekt Verzeichnis existiert keine Mobirise Projekt-Datei.");

        // Delete the project.mobirise file
        File.Delete(projectFile);
    }

    public static void AttachCustomCommentToFiles(string path, string customComment)
    {
        // Normalize the path
        var normalizedPath = Path.GetFullPath(path);

        // Check if the directory exists
        if(!Directory.Exists(normalizedPath))
            throw new DirectoryNotFoundException("Das angegebene Projekt Verzeichnis existiert nicht.");

        // Get all HTML files in the directory and its subdirectories
        var htmlFiles = Directory.GetFiles(normalizedPath, "*.html", SearchOption.AllDirectories);
        // Process each HTML file
        foreach(var htmlFile in htmlFiles)
        {
            // Get the content of the HTML file
            var content = File.ReadAllText(htmlFile);

            // Check if the content contains a closing </body> tag else -> continue to the next file
            if(!Regex.IsMatch(content, "</body>", RegexOptions.IgnoreCase | RegexOptions.IgnoreCase))
                continue;

            // Build the comment block with line breaks for readability
            var customCommentBlock = $"<!--\n{customComment}\n-->\n";

            // Insert the comment directly before the closing </body> tag
            var editedHtmlFile = Regex.Replace(content, "</body>", customCommentBlock + "</body>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            // Save the modified content back to the HTML file
            File.WriteAllText(htmlFile, editedHtmlFile);
        }
    }
}