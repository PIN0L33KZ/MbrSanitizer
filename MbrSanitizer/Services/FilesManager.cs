using System.Text.RegularExpressions;

namespace MbrSanitizer.Services;

internal class FilesManager
{
    public static void DeleteAllProjectFiles(string path)
    {
        try
        {
            var normalizedPath = Path.GetFullPath(path);

            //
            // Delete all files
            //
            var files = Directory.GetFiles(normalizedPath, "*", SearchOption.AllDirectories);

            foreach(var file in files)
                File.Delete(file);

            //
            // Delete all directories including childs
            //
            var directories = Directory.GetDirectories(normalizedPath, "*", SearchOption.AllDirectories);
            Array.Reverse(directories); // Reverse the array to delete child directories before parent directories

            foreach(var directory in directories)
                Directory.Delete(directory);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Konnte das Projekt Verzeichnis nicht aufräumen: {ex.Message}", "Mobirise Sanitizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void DeleteMbrFile(string path)
    {
        try
        {
            var normalizedPath = Path.GetFullPath(path);
            var projectFile = Path.Combine(normalizedPath, "project.mobirise");

            if(File.Exists(projectFile))
                File.Delete(projectFile);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Konnte die Mobirise-Projekt Datei nicht löschen: {ex.Message}", "Mobirise Sanitizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void AttachCustomCommentToFiles(string path, string customComment)
    {
        try
        {
            var normalizedPath = Path.GetFullPath(path);

            if(!Directory.Exists(normalizedPath))
                throw new DirectoryNotFoundException("Das angegebene Projekt Verzeichnis existiert nicht.");

            var htmlFiles = Directory.GetFiles(normalizedPath, "*.html", SearchOption.AllDirectories);

            foreach(var htmlFile in htmlFiles)
            {
                var content = File.ReadAllText(htmlFile);

                if(!Regex.IsMatch(content, "</body>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    continue;

                // Build the comment block with line breaks for readability
                var customCommentBlock = $"<!--\n{customComment}\n-->\n";

                // Insert the comment directly before the closing </body> tag (case-insensitive)
                var editedHtmlFile = Regex.Replace(content, "</body>", customCommentBlock + "</body>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                File.WriteAllText(htmlFile, editedHtmlFile);
            }

        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Konnte den benutzerdefinierten Kommentar nicht zu den Dateien hinzufügen: {ex.Message}", "Mobirise Sanitizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}