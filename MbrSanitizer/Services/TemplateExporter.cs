using MbrSanitizer.Data;
using Newtonsoft.Json;
namespace MbrSanitizer.Services;

internal class TemplateExporter
{
    public static void ExportTemplate(Template template, string exportPath)
    {
        // Serialize the template to JSON with indentation for readability
        var json = JsonConvert.SerializeObject(template, Formatting.Indented);
        // Write the JSON to the specified file path
        File.WriteAllText(exportPath, json);
    }
}