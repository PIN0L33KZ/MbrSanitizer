using MbrSanitizer.Data;
using Newtonsoft.Json;
namespace MbrSanitizer.Services;

internal class TemplateExporter
{
    public static void ExportTemplate(Template template, string exportPath)
    {
        try
        {
            var json = JsonConvert.SerializeObject(template, Formatting.Indented);
            File.WriteAllText(exportPath, json);
        }
        catch(Exception ex)
        {
            _ = MessageBox.Show($"Template konnte nicht exportiert werden: {ex.Message}", "Mobirise Sanitizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}