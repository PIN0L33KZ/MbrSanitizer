using MbrSanitizer.Data;

namespace MbrSanitizer.Services;

internal class InputSanitizer
{
    public static string SanitizeText(string input)
    {
        // Remove leading and trailing whitespace
        var sanitized = input.Trim();
        // Replace multiple spaces with a single space
        sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"\s+", " ");
        // Remove any non-printable characters
        sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"[^\x20-\x7E]", string.Empty);

        return sanitized;
    }

    public static bool IsValidText(string input)
    {
        // Check if the input is not null or empty
        if(string.IsNullOrEmpty(input))
        {
            return false;
        }

        // Check if the input contains only printable characters
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\x20-\x7E]+$");
    }

    public static bool IsValidTemplate(Template template)
    {
        var isValid = true;

        if(!IsValidText(template.ProjectPath) || template.ProjectPath.Length == 0)
            isValid = false;

        if(!IsValidText(template.ValueShort) || template.ValueShort.Length == 0)
            isValid = false;

        if(!IsValidText(template.ValueLong) || template.ValueLong.Length == 0)
            isValid = false;

        return isValid;
    }

    public static Template SanitizeTemplate(Template template)
    {
        Template tmpTemplate = new()
        {
            ProjectPath = SanitizeText(template.ProjectPath),
            ValueShort = SanitizeText(template.ValueShort),
            ValueLong = SanitizeText(template.ValueLong),
            CustomComment = template.CustomComment,
            DeleteMbrFile = template.DeleteMbrFile,
            AntiDragImgs = template.AntiDragImgs
        };

        return tmpTemplate;
    }
}