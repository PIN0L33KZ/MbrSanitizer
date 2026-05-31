using System.Reflection;

namespace MbrSanitizer.Application;

public static class Const
{
    public const string AppName = "Mobirise Sanitizer";
    public static string RemovePatternPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"1. Webseiten\patterns.txt");
    public static string AppVersion => Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version?
                .ToString()
                ?? "Unknown"; // Return "Unknown" if the version cannot be determined
}