using MbrSanitizer.Data;
using MbrSanitizer.Helper;
using Newtonsoft.Json;

namespace MbrSanitizer.Services;

internal class TemplatesManagerService
{
    public static void ExportTemplate(Template template, string exportPath)
    {
        // Serialize the template to JSON with indentation for readability
        var json = JsonConvert.SerializeObject(template, Formatting.Indented);
        // Write the JSON to the specified file path
        File.WriteAllText(exportPath, json);
    }

    public static Template ImportTemplate(string importPath)
    {
        // Read the JSON file
        var json = File.ReadAllText(importPath);
        // Deserialize it into a Template object
        Template? tmpTemplate = JsonConvert.DeserializeObject<Template>(json);

        // Return Template object
        // If deserialization fails -> return default Template object
        return tmpTemplate ?? new Template()
        {
            ProjectPath = "",
            ValueShort = "",
            ValueLong = "",
            DeleteMbrFile = false,
            AntiDragImgs = false,
            CustomComment = ""
        };
    }

    public static Template SanitizeTemplate(Template template)
    {
        Template tmpTemplate = new()
        {
            ProjectPath = InputSanitizer.SanitizeText(template.ProjectPath),
            ValueShort = InputSanitizer.SanitizeText(template.ValueShort),
            ValueLong = InputSanitizer.SanitizeText(template.ValueLong),
            CustomComment = template.CustomComment,
            DeleteMbrFile = template.DeleteMbrFile,
            AntiDragImgs = template.AntiDragImgs
        };

        return tmpTemplate;
    }

    public static Template GetDefaultTemplate()
    {
        return new Template()
        {
            ProjectPath = "",
            ValueShort = "",
            ValueLong = "",
            DeleteMbrFile = false,
            AntiDragImgs = false,
            CustomComment = ""
        };
    }
}