using System.Reflection;

namespace MbrSanitizer.Application;

public static class Const
{
    public const string AppName = "Mobirise Sanitizer";
    public static string AppVersion => Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version?
                .ToString()
                ?? "Unknown"; // Return "Unknown" if the version cannot be determined
}