using MbrSanitizer.Data;
using Newtonsoft.Json;

namespace MbrSanitizer.Services;

internal class TemplateImporter
{
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
}