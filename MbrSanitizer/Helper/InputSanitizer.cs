using MbrSanitizer.Data;

namespace MbrSanitizer.Helper;

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
            return false;

        // Check if the input contains only printable characters
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\x20-\x7E]+$");
    }

    public static bool IsValidTemplate(Template template)
    {
        // Check if the template is not null
        if(template == null)
            return false;

        if(!IsValidText(template.ProjectPath) || template.ProjectPath.Length == 0)
            return false;

        if(!IsValidText(template.ValueShort) || template.ValueShort.Length == 0)
            return false;

        if(!IsValidText(template.ValueLong) || template.ValueLong.Length == 0)
            return false;

        // Return true if all checks passed
        return true;
    }
}