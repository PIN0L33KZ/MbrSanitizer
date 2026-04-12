using MbrSanitizer.Data;
using Newtonsoft.Json;

namespace MbrSanitizer.Services;

internal class TemplateImporter
{
    public static Template ImportTemplate(string importPath)
    {
        try
        {
            var json = File.ReadAllText(importPath);
            Template? tmpTemplate = JsonConvert.DeserializeObject<Template>(json);

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
        catch
        {
            _ = MessageBox.Show($"Die Vorlage konnte nicht geladen werden.", "Mobirise Sanitizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}